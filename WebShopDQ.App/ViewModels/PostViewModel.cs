using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShopDQ.App.ViewModels
{
    public class PostViewModel
    {
        public string? Title { get; set; }
        public string? UrlImage { get; set; }
        public double Price { get; set; }
    }

    public class PostListViewModel
    {
        public int TotalPost { get; set; }
        public ICollection<PostViewModel>? PostList { get; set; }
    }
}
