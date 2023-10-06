using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShopDQ.App.Models;

namespace WebShopDQ.App.ViewModels
{
    public class PostViewModel
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string? AvatarUrl { get; set; }
        public string? CategoryName { get; set; }
        public string? Title { get; set; }
        public string? UrlImage { get; set; }
        public double Price { get; set; }
        public string? Status { get; set; }
    }

    public class PostListViewModel
    {
        public int TotalPost { get; set; }
        public ICollection<PostViewModel>? PostList { get; set; }
    }
}
