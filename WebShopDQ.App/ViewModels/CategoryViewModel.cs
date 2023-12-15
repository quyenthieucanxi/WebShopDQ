using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShopDQ.App.Models;

namespace WebShopDQ.App.ViewModels
{
    public class CategoryViewModel : BaseVM
    {
        public string? CategoryName { get; set; }
        public string? CategoryPath { get; set; }
        public string? urlImg { get; set; }
    }

    public class CategoryListViewModel
    {
        public int TotalCategory { get; set; }
        public ICollection<CategoryViewModel>? CategoryList { get; set; }
    }
}
