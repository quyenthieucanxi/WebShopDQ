using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShopDQ.App.Models;

namespace WebShopDQ.App.ViewModels
{
    public class OrderViewModel : BaseVM
    {
        public int Quantity { get; set; }
        public float TotalPrice { get; set; }
        public string Status { get; set; } = null!;
        public string Payment { get; set; } = null!;
        public string? Note { get; set; }
        public string RecipientName { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string AddressShipping { get; set; } = null!;
        public UserInfoViewModel? User { get; set; }
        public ICollection<PostViewModel>? Products { get; set; } = null!;
    }

    public class OrderListViewModel
    {
        public int TotalOrder { get; set; }
        public ICollection<OrderViewModel>? OrderList { get; set; }
    }
}
