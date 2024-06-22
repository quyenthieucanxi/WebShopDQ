using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShopDQ.App.Models
{
    public class OrderDetail : BaseModel
    {
        public OrderDetail() { }
        
        public OrderDetail(Guid id) : base(id) { 
        }
        public Guid OrderId { get; set; }
        public Guid ProductId { get; set; }
        public Post Post { get; set; } = null!;

    }
}
