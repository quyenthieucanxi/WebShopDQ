using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShopDQ.App.Common.Constant;
using WebShopDQ.App.Data;
using WebShopDQ.App.Models;
using WebShopDQ.App.Repositories.IRepositories;

namespace WebShopDQ.App.Repositories
{
    public class OrderReviewRepository : Repository<OrderReviews>, IOrderReviewRepository
    {
        private readonly IPostRepository _postRepository;
        public OrderReviewRepository(DatabaseContext databaseContext, IPostRepository postRepository) : base(databaseContext)
        {
            _postRepository = postRepository;
        }

        public async Task<List<OrderReviews>?> CountOrderReview(Guid userID)
        {
            var products = await _postRepository.FindAllAsync(p => p.UserID == userID);
            var orders = await Entities.Include(or => or.Order)
                                        .ThenInclude(o => o.Products)                        
                                        .ToListAsync();
            var ordersReview = orders.Where(o => products.Any(p => p.Id == o.Order.ProductID));
            return ordersReview.ToList();
                          
        }

        public async Task<List<OrderReviews>?> GetOrderReviews(Guid productID,int? rating)
        {
            IQueryable<OrderReviews> query = Entities.Include(or => or.Order)
                                             .ThenInclude(o => o.UserOrder)
                                             .Include(or => or.Order)
                                             .ThenInclude(o => o.Products)
                                             .OrderByDescending(or => or.CreatedTime);

            if (rating.HasValue)
            {
                query = query.Where(or => or.Order.ProductID == productID && or.Rating == rating);
            }
            else
            {
                query = query.Where(or => or.Order.ProductID == productID);
            }

            var ordersReviews = await query.ToListAsync();
            return ordersReviews;
        }
    }
}
