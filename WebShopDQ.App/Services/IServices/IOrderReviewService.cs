using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShopDQ.App.DTO;
using WebShopDQ.App.ViewModels;

namespace WebShopDQ.App.Services.IServices
{
    public interface IOrderReviewService
    {
        Task<bool> Create(OrderReviewDTO orderReviewDTO, Guid userId);
        Task<ValueReviewViewModel> CountOrderReview(string url);
        Task<ICollection<OrderReviewsViewModel>> GetOrderReviews(string pathPost,int? rating);
        Task<ValueReviewViewModel> CountProductReview(string pathPost);
        Task<ICollection<OrderReviewsViewModel>> GetReviewsUser(string url);
    }
}
