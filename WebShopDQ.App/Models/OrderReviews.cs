using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShopDQ.App.Models
{   
    public class OrderReviews : BaseModel
    {
        public OrderReviews() { }
        public OrderReviews(Guid id) : base(id)
        {
        }

        public Guid UserId { get; set; }
        public Guid OrderId { get; set; }
        public int Rating { get; set; } 
        public string? ReviewText { get; set; }
        public User? User { get; set; }
        public Order? Order { get; set; }
     }
}
