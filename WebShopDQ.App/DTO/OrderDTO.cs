using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShopDQ.App.DTO
{
    public class OrderDTO
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public float TotalPrice { get; set; }
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Note { get; set; }
    }
}
