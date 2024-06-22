using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShopDQ.App.Models;

namespace WebShopDQ.App.Repositories.IRepositories
{
    public interface IOrderReviewRepository : IRepository<OrderReviews>
    {
        Task<List<OrderReviews>?> CountOrderReview(Guid userID);
        Task<List<OrderReviews>?> GetOrderReviews(Guid  productID,int? rating);
    }
}
