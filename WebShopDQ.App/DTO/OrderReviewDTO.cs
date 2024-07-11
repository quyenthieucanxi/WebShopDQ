using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShopDQ.App.DTO
{
    public class OrderReviewDTO
    {
        public Guid OrderId { get; set; }
        public int Rating { get; set; }
        public string? ReviewText { get; set; }
    }
}
