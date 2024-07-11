using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShopDQ.App.Models;

namespace WebShopDQ.App.ViewModels
{
    public class ValueReviewViewModel
    {
        public int totalReview { get; set; }
        public double averageRating { get; set; }
    }
    public class OrderReviewsViewModel : BaseVM
    {
        public int Rating { get; set; }
        public string? ReviewText { get; set; }
        public UserInfoViewModel? User { get; set; }
        public ICollection<PostViewModel>? Products { get; set; }
    }
}
