﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShopDQ.App.DTO;
using WebShopDQ.App.ViewModels;

namespace WebShopDQ.App.Services.IServices
{
    public interface IOrderService
    {
        Task<bool> Create(OrderDTO orderDTO, Guid userId);
        Task<OrderListViewModel> GetAll(Guid userId);
        Task<OrderListViewModel> GetAllBySeller(Guid userId, string? status);
        Task<List<WeeklyRevenue>> GetAllRevenueInMonth(Guid userId, int month, int year);
        Task<List<DailyRevenue>> GetAllRevenueInWeek(Guid userId, DateTime start, DateTime end);
        Task<OrderViewModel> GetById(Guid orderId);
        Task<OrderListViewModel> GetByStatus(int page, int limit, string status, Guid userId);
        Task<OrderListViewModel> GetByStatus(string status, Guid userId);
        Task<bool> UpdateStatus(Guid orderId, string status, Guid userId);
    }
}
