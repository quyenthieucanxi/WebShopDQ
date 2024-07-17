using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShopDQ.App.Models;

namespace WebShopDQ.App.ViewModels
{
    public class PostViewModel : BaseVM
    {
        public Guid UserId { get; set; }
        public string? AvatarUrl { get; set; }
        public string? CategoryName { get; set; }
        public string? CategoryPath { get; set; }
        public string? PostPath { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? UrlImage { get; set; }
        public bool IsTrend { get; set; } 
        public string? requestTrend { get; set; }
        public double Price { get; set; }
        public string? Address { get; set; }
        public string? Status { get; set; }
        public int Quantity { get; set; }
        public UserInfoViewModel? User { get; set; }
        public List<FilesViewModel>? Files { get; set; }
    }

    public class PostListViewModel
    {
        public int TotalPost { get; set; }
        public ICollection<PostViewModel>? PostList { get; set; }
    }
}
