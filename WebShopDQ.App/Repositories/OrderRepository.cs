using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShopDQ.App.Data;
using WebShopDQ.App.Models;
using WebShopDQ.App.Repositories.IRepositories;
using WebShopDQ.App.ViewModels;

namespace WebShopDQ.App.Repositories
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        private readonly IMapper _mapper;


        public OrderRepository(DatabaseContext databaseContext, IMapper mapper) : base(databaseContext)
        {
            _mapper = mapper;
        }

        public async Task<OrderListViewModel> GetByStatus(int page, int limit, string status, Guid userId)
        {
            try
            {
                var query = Entities.Include(p => p.Product)
                                    .Include(p => p.User);
                page = page != 0 ? page : 1;
                limit = limit != 0 ? limit : 4;
                var listData = new List<OrderViewModel>();
                var data = await query.OrderByDescending(post => post.CreatedTime)
                    .Where(p => p.Status == status && p.UserId == userId).ToListAsync();
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
    }
}
