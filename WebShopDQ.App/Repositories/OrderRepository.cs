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
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.Identity;
using Hangfire;
using WebShopDQ.App.Services;

namespace WebShopDQ.App.Repositories
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        private readonly IMapper _mapper;
        private readonly IPostRepository _postRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserRepository _userRepository;
        private readonly UserManager<User> _userManager;
        private readonly DatabaseContext _databaseContext;
        private readonly IBackgroundJobClient _backgroundJobClient;
        public OrderRepository(
            DatabaseContext databaseContext,
            IMapper mapper,
            IPostRepository postRepository,
            IUnitOfWork unitOfWork,
            IUserRepository userRepository,
            UserManager<User> userManager,
            IBackgroundJobClient backgroundJobClient) : base(databaseContext)
        {
            _mapper = mapper;
            _postRepository = postRepository;
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
            _userManager = userManager;
            _databaseContext = databaseContext;
            _backgroundJobClient = backgroundJobClient;
        }

        public async Task<bool> Create(OrderDTO orderDTO, Guid userId)
        {
            var post = await _postRepository.GetById(orderDTO.ProductId) 
                ?? throw new KeyNotFoundException(Messages.PostNotFound);
            if (orderDTO.Quantity > post.Quantity)
            {
                throw new ValidateException(Messages.QuantityInvalid);
            }
            try
            {
                _unitOfWork.BeginTransaction();
                var order = _mapper.Map<Order>(orderDTO);
                order.UserID = userId;
                await Add(order);
                post.Quantity = post.Quantity - orderDTO.Quantity;
                await _postRepository.Update(post);
                await _unitOfWork.SaveChanges();
                _unitOfWork.CommitTransaction();
                _backgroundJobClient.Enqueue<NotifyService>(service => service.NotifyWhenUserCreateOrder(userId,post.UserID,post.Title));
                return await Task.FromResult(true);
            }
            catch (Exception ex)
            {
                _unitOfWork.RollbackTransaction();
                throw new Exception(ex.Message);
            }
        }

        public async Task<OrderListViewModel> GetAllVM(Guid userId)
        {
            var data = await FindAllAsync(p => p.UserID == userId, new string[] { "UserOrder", "AddressShipping", "Products" });
            var orders = data.OrderByDescending(p => p.CreatedTime);
            foreach (var order in orders)
            {
                var product = await _postRepository.FindAsync(p=> p.Id == order.ProductID,new string[] {nameof(Post.User)});
                order.Products?.Add(product!);
            }
            var OrderList = _mapper.Map<List<OrderViewModel>>(orders);
            return new OrderListViewModel
            {
                TotalOrder = OrderList.Count,
                OrderList = OrderList,
            };
        }

        public async Task<OrderListViewModel> GetAllVMBySeller(Guid userId)
        {
            var data = await Entities.Include(o => o.Products!)
                                     .Include(o => o.UserOrder)
                                     .Include(o => o.AddressShipping).ToListAsync();
            var orders = data.OrderByDescending(p => p.CreatedTime);
            foreach (var order in orders)
            {
                var product = await _postRepository.GetById(order.ProductID);
                if (product!.UserID == userId)
                {
                    order.Products?.Add(product!);
                }
            }
            var OrderList = _mapper.Map<List<OrderViewModel>>(orders.Where(o => o.Products!.Count > 0));
            return new OrderListViewModel
            {
                TotalOrder = OrderList.Count,
                OrderList = OrderList,
            };
        }

        public async Task<OrderListViewModel> GetByStatus(int page, int limit, string status, Guid userId)
        {
            try
            {
                var query = Entities.Include(order => order.UserOrder)
                                 .Include(order => order.Products)
                                 .Include(order => order.AddressShipping);

                var data = await query.OrderByDescending(post => post.CreatedTime)
                    .Where(p => p.Status == status && p.UserID == userId).ToListAsync();
                var totalCount = data.Count;
                data = data.Skip((page - 1) * limit).Take(limit).ToList();
                foreach (var order in data)
                {
                    var product = await _postRepository.GetById(order.ProductID);
                    order.Products?.Add(product!);
                }
                var OrderList = _mapper.Map<ICollection<OrderViewModel>>(data);
                return new OrderListViewModel
                {
                    TotalOrder = totalCount,
                    OrderList = OrderList
                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<OrderListViewModel> GetByStatus(string status, Guid userId)
        {
            var query =  Entities.Include(order => order.UserOrder)
                                         .Include(order => order.Products)
                                         .Include(order => order.AddressShipping);
            var data = await query.Where(p => p.Status!.ToLower() == status.ToLower() && p.UserID == userId).ToListAsync();
            var total = data.Count;
            foreach (var order in data)
            {
                var product = await _postRepository.GetById(order.ProductID);
                order.Products?.Add(product!);
            }
            var OrderList = _mapper.Map<ICollection<OrderViewModel>>(data.OrderByDescending(p => p.CreatedTime));
            return new OrderListViewModel
            {
                TotalOrder = total,
                OrderList = OrderList
            };
        }
    }
}
