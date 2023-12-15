using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShopDQ.App.DTO;
using WebShopDQ.App.Models;
using WebShopDQ.App.ViewModels;

namespace WebShopDQ.App.Repositories.IRepositories
{
    public interface IOrderRepository : IRepository<Order>
    {
        Task<bool> Create(OrderDTO orderDTO, Guid userId);
        Task<OrderListViewModel> GetByStatus(int page, int limit, string status, Guid userId);
        Task<OrderListViewModel> GetByStatus(string status, Guid userId);
        Task<OrderListViewModel> GetAllVM();
    }
}
