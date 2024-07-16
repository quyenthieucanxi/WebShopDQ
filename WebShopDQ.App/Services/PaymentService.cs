using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Org.BouncyCastle.Asn1.Ocsp;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using WebShopDQ.App.Common.Constant;
using WebShopDQ.App.DTO;
using WebShopDQ.App.Models;
using WebShopDQ.App.Payment;
using WebShopDQ.App.Repositories.IRepositories;
using WebShopDQ.App.Services.IServices;
using WebShopDQ.App.ViewModels;
using WebShopDQ.App.Common;
using AutoMapper;
using Serilog.Core;
using WebShopDQ.App.Data;
using WebShopDQ.App.Common.Exceptions;
using Hangfire;

namespace WebShopDQ.App.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IConfiguration _configuration ;
        private readonly IOrderRepository _orderRepository;
        private readonly IPostRepository _postRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;
        private readonly IBackgroundJobClient _backgroundJobClient;
        private readonly IUnitOfWork _unitOfWork;

        public PaymentService(IConfiguration configuration, IOrderRepository orderRepository,
                                IHttpContextAccessor httpContextAccessor,
                                IMapper mapper, IUnitOfWork unitOfWork, IPostRepository postRepository, IBackgroundJobClient backgroundJobClient)
        {
            _configuration = configuration;
            _orderRepository = orderRepository;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _postRepository = postRepository;
            _backgroundJobClient = backgroundJobClient;
        }

        public async Task<string> CreateUrlPayment(OrderDTO orderDTO, Guid userId)
        {
            var vnp_Returnurl = _configuration["VnPay:Returnurl"]; //URL nhan ket qua tra ve 
            var vnp_Url = _configuration["VnPay:Url"]; //URL thanh toan cua VNPAY 
            var vnp_TmnCode = _configuration["VnPay:TmmCode"]; //Ma định danh merchant kết nối (Terminal Id)
            var vnp_HashSecret = _configuration["VnPay:HashSecret"]; //Secret Key
            var version = _configuration["VnPay:Version"];

            var order = _mapper.Map<Order>(orderDTO);
            var orderId = Guid.NewGuid();
            order.Id = orderId;
            order.UserID = userId;
            await _orderRepository.Add(order);
            VnPayLibrary vnpay = new VnPayLibrary();

            vnpay.AddRequestData("vnp_Version", version!);
            vnpay.AddRequestData("vnp_Command", "pay");
            vnpay.AddRequestData("vnp_TmnCode", vnp_TmnCode!);
            vnpay.AddRequestData("vnp_Amount", (order.TotalPrice * 100).ToString());
            vnpay.AddRequestData("vnp_CreateDate", DateTime.Now.ToString("yyyyMMddHHmmss"));
            vnpay.AddRequestData("vnp_CurrCode", "VND");
            vnpay.AddRequestData("vnp_IpAddr", Payment.Utils.GetIpAddress(_httpContextAccessor));
            vnpay.AddRequestData("vnp_Locale", "vn");
            vnpay.AddRequestData("vnp_OrderInfo", "Thanh toan don hang:" + orderId);
            vnpay.AddRequestData("vnp_OrderType", "other"); //default value: other
            vnpay.AddRequestData("vnp_ReturnUrl", vnp_Returnurl!);
            vnpay.AddRequestData("vnp_TxnRef", orderId.ToString()); // Mã tham chiếu của giao dịch tại hệ thống của merchant. Mã này là duy nhất dùng để phân biệt các đơn hàng gửi sang VNPAY. Không được trùng lặp trong ngày
            string paymentUrl = vnpay.CreateRequestUrl(vnp_Url!, vnp_HashSecret!);
            return await Task.FromResult(paymentUrl);
        }
        public async Task<PaymentViewModel> CallbackPayment(VNPayDTO vNPayDTO, Guid userId)
        {
            VnPayLibrary vnpay = new VnPayLibrary();
            foreach (var property in vNPayDTO.GetType().GetProperties())
            {
                var key = property.Name;
                var value = property.GetValue(vNPayDTO);
                if (!string.IsNullOrEmpty(key) && key.StartsWith("vnp_"))
                {
                    vnpay.AddResponseData(key,value.ToString().Trim());
                }
            }
            var vnp_HashSecret = _configuration["VnPay:HashSecret"];
            var orderId = vnpay.GetResponseData("vnp_TxnRef");
            long vnp_Amount = Convert.ToInt64(vnpay.GetResponseData("vnp_Amount")) / 100;
            long vnpayTranId = Convert.ToInt64(vnpay.GetResponseData("vnp_TransactionNo"));
            string vnp_ResponseCode = vnpay.GetResponseData("vnp_ResponseCode");
            string vnp_TransactionStatus = vnpay.GetResponseData("vnp_TransactionStatus");
            string vnp_SecureHash = vnpay.GetResponseData("vnp_SecureHash");
            bool checkSignature = vnpay.ValidateSignature(vnp_SecureHash, vnp_HashSecret!);
            if (checkSignature)
            {
                var order = await _orderRepository.GetById(Guid.Parse(orderId)) ?? throw new KeyNotFoundException(Messages.OrderNotFound); 
                if (order?.Payment == PaymentStatus.Pending)
                {
                    try
                    {
                        _unitOfWork.BeginTransaction();
                        var post = await _postRepository.GetById(order.ProductID);
                        if (vnp_ResponseCode == "00" && vnp_TransactionStatus == "00")
                        {
                            Log.Information($"Thanh toan thanh cong, OrderId={0}, VNPAY TranId={1}", orderId, vnpayTranId);
                            order.Payment = PaymentStatus.VNpay;
                            var quantity = post!.Quantity - order.Quantity;
                            post.Quantity = quantity >= 0 ? quantity : throw new ValidateException(Messages.QuantityInvalid); ;
                            await _postRepository.Update(post);
                        }
                        else
                        {
                            Log.Information("Thanh toan loi, OrderId={0}, VNPAY TranId={1},ResponseCode={2}",
                                                orderId, vnpayTranId, vnp_ResponseCode);
                            order.Payment = PaymentStatus.Error;
                        }
                        await _orderRepository.Update(order);
                        await _unitOfWork.SaveChanges();
                        _unitOfWork.CommitTransaction();
                        _backgroundJobClient.Enqueue<NotifyService>(service => service.NotifyWhenUserCreateOrder(userId, post.UserID, post.Title));

                    }
                    catch (Exception ex)
                    {
                        _unitOfWork.RollbackTransaction();
                        throw new Exception(ex.Message);
                    }
                    
                    return new PaymentViewModel { RspCode = "00" , Message = "Payment Success" };
                }
                else
                {
                    return new PaymentViewModel { RspCode = "02", Message = "Order already payment" };
                }
            }
            else
            {
                Log.Information("Invalid signature");
                return new PaymentViewModel { RspCode = "97", Message = "Invalid signature" };
            }
        }
        
    }
}
