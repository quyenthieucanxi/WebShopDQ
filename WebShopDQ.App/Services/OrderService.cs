﻿using AutoMapper;
using CloudinaryDotNet.Actions;
using Hangfire;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Globalization;
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
        private readonly IBackgroundJobClient _backgroundJobClient;
        public OrderService(
            IUserRepository userRepository,
            IOrderRepository orderRepository,
            IPaymentService paymentService,
            IMapper mapper,
            IPostRepository postRepository,
            IBackgroundJobClient backgroundJobClient)
        {
            _userRepository = userRepository;
            _orderRepository = orderRepository;
            _paymentService = paymentService;
            _mapper = mapper;
            _postRepository = postRepository;
            _backgroundJobClient = backgroundJobClient;
        }
        public async Task<bool> Create(OrderDTO orderDTO, Guid userId)
        {
            await _orderRepository.Create(orderDTO, userId);

            return await Task.FromResult(true);
        }

        public async Task<OrderListViewModel> GetAll(Guid userId) => await _orderRepository.GetAllVM(userId);

        public async Task<OrderListViewModel> GetAllBySeller(Guid userId, string? status) => await _orderRepository.GetAllVMBySeller(userId, status);

        public async Task<List<WeeklyRevenue>> GetAllRevenueInMonth(Guid userId, int month, int year)
        {
            var orders = await GetAllBySeller(userId, null);
            var firstDayOfMonth = new DateTime(year, month, 1);
            var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);

            var weeklyRevenues = orders.OrderList
                .Where(o => o.CreatedTime >= firstDayOfMonth && o.CreatedTime <= lastDayOfMonth)
                .GroupBy(o => CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(o.CreatedTime, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday))
                .Select(g => new WeeklyRevenue
                {
                    Week = $"Week {g.Key}",
                    Revenue = g.Sum(o => o.TotalPrice)
                }).ToList();
            return weeklyRevenues;
        }

        public async Task<List<DailyRevenue>> GetAllRevenueInWeek(Guid userId, DateTime start, DateTime end)
        {
            var orders = await GetAllBySeller(userId, null);
            var dailyRevenue = orders.OrderList.Where(o => o.CreatedTime >= start && o.CreatedTime <= end)
                                    .GroupBy(o => o.CreatedTime.Date)
                                    .Select(g => new DailyRevenue
                                    {
                                        Date = g.Key.ToString("yyyy-MM-dd"),
                                        Revenue = g.Sum(o => o.TotalPrice)
                                    }).ToList();
            return dailyRevenue;

        }

        public async Task<OrderViewModel> GetById(Guid orderId)
        {
            string[] orderInclude = { nameof(Order.AddressShipping), nameof(Order.Products) };
            var order = await _orderRepository.FindAsync(o => o.Id == orderId, orderInclude) ?? throw new KeyNotFoundException(Messages.OrderNotFound);
            string[] productInclude = { nameof(Post.User) };
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

        public async Task<bool> UpdateStatus(Guid orderId, string status, Guid userId)
        {
            var order = await _orderRepository.GetById(orderId) ?? throw new KeyNotFoundException(Messages.OrderNotFound);
            order.Status = status;
            await _orderRepository.Update(order);
            string[] productInclude = { nameof(Post.User) };
            var product = await _postRepository.FindAsync(p => p.Id == order.ProductID, productInclude);
            _backgroundJobClient.Enqueue<NotifyService>(service => service.NotifyWhenSellerUpdateStatusOrder(userId, order.UserID, product.User.Id, product!.Title, product!.User!.FullName, product!.User!.AvatarUrl, status));
            return await Task.FromResult(true);
        }
    }
}
