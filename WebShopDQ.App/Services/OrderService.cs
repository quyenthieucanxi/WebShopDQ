using AutoMapper;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShopDQ.App.Common;
using WebShopDQ.App.Common.Constant;
using WebShopDQ.App.Common.Exceptions;
using WebShopDQ.App.Data;
using WebShopDQ.App.DTO;
using WebShopDQ.App.Models;
using WebShopDQ.App.Repositories;
using WebShopDQ.App.Repositories.IRepositories;
using WebShopDQ.App.Services.IServices;
using WebShopDQ.App.ViewModels;

namespace WebShopDQ.App.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IUserRepository _userRepository;
        private readonly IPaymentService _paymentService;
        private readonly IPostRepository _postRepository;
        private readonly IMapper _mapper;
        public OrderService(IUserRepository userRepository, IOrderRepository orderRepository, 
                        IPaymentService paymentService, IMapper mapper, IPostRepository postRepository)
        {
            _userRepository = userRepository;
            _orderRepository = orderRepository;
            _paymentService = paymentService;
            _mapper = mapper;
            _postRepository = postRepository;
        }
        public async Task<bool> Create(OrderDTO orderDTO, Guid userId)
        {
            return await _orderRepository.Create(orderDTO, userId);
        }

        public async Task<OrderListViewModel> GetAll(Guid userId) => await _orderRepository.GetAllVM(userId);

        public async Task<OrderViewModel> GetById(Guid orderId)
        {
            string[] orderInclude = { "AddressShipping", "Products" };
            var order = await _orderRepository.FindAsync(o => o.Id == orderId, orderInclude) ?? throw new KeyNotFoundException(Messages.OrderNotFound);
            string[] productInclude = { "User" };
            var product = await _postRepository.FindAsync(p => p.Id == order.ProductID, productInclude);
            order.Products?.Add(product!);
            var orderVM = _mapper.Map<OrderViewModel>(order);
            return await Task.FromResult(orderVM);
        }

        public async Task<OrderListViewModel> GetByStatus(int page, int limit, string status, Guid userId)
        {
            return await _orderRepository.GetByStatus(page, limit, status, userId);
        }

        public async Task<OrderListViewModel> GetByStatus(string status, Guid userId)
        {
            return await _orderRepository.GetByStatus(status, userId);
        }

        public async Task<bool> UpdateStatus(Guid orderId, string status)
        {
            var order = await _orderRepository.GetById(orderId) ?? throw new KeyNotFoundException(Messages.OrderNotFound);
            order.Status = status;
            await _orderRepository.Update(order);
            return await Task.FromResult(true);
        }
    }
}
