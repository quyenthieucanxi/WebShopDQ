using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Threading.Tasks;
using WebShopDQ.App.Common;
using WebShopDQ.App.Common.Exceptions;
using WebShopDQ.App.Data;
using WebShopDQ.App.DTO;
using WebShopDQ.App.Models;
using WebShopDQ.App.Repositories.IRepositories;
using WebShopDQ.App.ViewModels;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace WebShopDQ.App.Repositories
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        private readonly IMapper _mapper;
        private readonly IPostRepository _postRepository;
        private readonly IUnitOfWork _unitOfWork;

        public OrderRepository(DatabaseContext databaseContext, IMapper mapper, IPostRepository postRepository, IUnitOfWork unitOfWork) : base(databaseContext)
        {
            _mapper = mapper;
            _postRepository = postRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Create(OrderDTO orderDTO, Guid userId)
        {
            var post = await _postRepository.GetById(orderDTO.ProductId) ?? throw new KeyNotFoundException(Messages.PostNotFound);
            if (orderDTO.Quantity > post.Quantity)
            {
                throw new ValidateException("Số lượng không hợp lệ");
            }
            try
            {    
                _unitOfWork.BeginTransaction();
                var order = _mapper.Map<Order>(orderDTO);
                order.UserID = userId;
                await Add(order);
                post.Quantity = (post.Quantity - orderDTO.Quantity);
                await _postRepository.Update(post);
                _unitOfWork.SaveChanges();
                _unitOfWork.CommitTransaction();
                return await Task.FromResult(true);
            }
            catch (Exception ex)
            {
                _unitOfWork.RollbackTransaction();
                throw new Exception(ex.Message);
            }
        }

        public async Task<OrderListViewModel> GetAllVM()
        {
            var data = await GetAllAsync(new string[] { "UserOrder", "AddressShipping", "Product", "Product.Category" });
            //var data = await  Entities
            //                     .Include(order => order.UserOrder)
            //                     .Include(order => order.Product)
            //                     .Include(order => order.AddressShipping)
            //                     .Select(order => new Order { Id = order.Id, Quantity = order.Quantity, TotalPrice = order.TotalPrice, Status = order.Status!, Payment = order.Payment, Note = order.Note, 
            //                         UserOrder = new User { Id = order.UserOrder!.Id, FullName = order.UserOrder.FullName, }, 
            //                         Product = new Post { Id = order.Product!.Id, Title = order.Product.Title, UrlImage = order.Product.UrlImage, Quantity = order.Product.Quantity,Price = order.Product.Price,Category = order.Product.Category }, 
            //                         AddressShipping = new AddressShipping { Id = order.AddressShipping.Id, RecipientName = order.AddressShipping.RecipientName, Phone = order.AddressShipping.Phone, Province = order.AddressShipping.Province, Distrist = order.AddressShipping.Distrist, AddressDetail = order.AddressShipping.AddressDetail, } })
            //                     .ToListAsync();    
            var total = data.Count();
            var OrderList = _mapper.Map<List<OrderViewModel>>(data);
            return new OrderListViewModel
            {
                TotalOrder = total,
                OrderList = OrderList,
            };
        }

        public async Task<OrderListViewModel> GetByStatus(int page, int limit, string status, Guid userId)
        {
            try
            {
                var query = Entities.Include(order => order.UserOrder)
                                 .Include(order => order.Product)
                                 .Include(order => order.AddressShipping);
                page = page != 0 ? page : 1;
                limit = limit != 0 ? limit : 4;
                var listData = new List<OrderViewModel>();
                var data = await query.OrderByDescending(post => post.CreatedTime)
                    .Where(p => p.Status == status && p.UserID == userId).ToListAsync();
                var totalCount = data.Count;
                data = data.Skip((page - 1) * limit).Take(limit).ToList();
                foreach (var item in data)
                {
                    var order = _mapper.Map<OrderViewModel>(item);
                    listData.Add(order);
                }
                return new OrderListViewModel
                {
                    TotalOrder = totalCount,
                    OrderList = listData
                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<OrderListViewModel> GetByStatus(string status, Guid userId)
        {
            string[] includes = { "UserOrder", "AddressShipping", "Product", "Product.Category" };
            var data = await FindAllAsync(p => p.Status!.ToLower() == status.ToLower()  && p.UserID == userId, includes);
            var total = data.Count();
            var OrderList = _mapper.Map<ICollection<OrderViewModel>>(data);
            return new OrderListViewModel
            {
                TotalOrder = total,
                OrderList = OrderList
            };
        }
    }
}
