using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        public OrderService(IUserRepository userRepository, IOrderRepository orderRepository)
        {
            _userRepository = userRepository;
            _orderRepository = orderRepository;

        }
        public async Task<bool> Create(OrderDTO orderDTO, Guid userId)
        {
            var user = await _userRepository.GetById(userId);
            try
            {
                var order = new Order
                {
                    //UserId = userId,
                    UserID = user!.Id,
                    ProductID = orderDTO.ProductId,
                    Address = orderDTO.Address,
                    PhoneNumber = orderDTO.PhoneNumber,
                    Note = orderDTO.Note,
                    Quantity = orderDTO.Quantity,
                    TotalPrice = orderDTO.TotalPrice
                };
                await _orderRepository.Add(order);
                return await Task.FromResult(true);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<OrderListViewModel> GetByStatus(int page, int limit, string status, Guid userId)
        {
            return await _orderRepository.GetByStatus(page, limit, status, userId);
        }
    }
}
