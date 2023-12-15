using AutoMapper;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        public OrderService(IUserRepository userRepository, IOrderRepository orderRepository)
        {
            _userRepository = userRepository;
            _orderRepository = orderRepository;

        }
        public async Task<bool> Create(OrderDTO orderDTO, Guid userId)
        {
            return await _orderRepository.Create(orderDTO, userId);
        }

        public async Task<OrderListViewModel> GetAll() => await _orderRepository.GetAllVM();

        public async Task<OrderListViewModel> GetByStatus(int page, int limit, string status, Guid userId)
        {
            return await _orderRepository.GetByStatus(page, limit, status, userId);
        }

        public async Task<OrderListViewModel> GetByStatus(string status, Guid userId)
        {
            return await _orderRepository.GetByStatus(status, userId);
        }
    }
}
