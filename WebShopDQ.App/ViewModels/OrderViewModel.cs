using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShopDQ.App.ViewModels
{
    public class OrderViewModel
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public float TotalPrice { get; set; }
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Note { get; set; }
        public string? Status { get; set; }
    }

    public class OrderListViewModel
    {
        public int TotalOrder { get; set; }
        public ICollection<OrderViewModel>? OrderList { get; set; }
    }
}
