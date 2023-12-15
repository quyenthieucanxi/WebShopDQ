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
        public Guid AddressShippingID { get; set; }
        public int Quantity { get; set; }
        public float TotalPrice { get; set; }
        public string Payment { get; set; } = null!;
        public string? Note { get; set; }
    }
}
