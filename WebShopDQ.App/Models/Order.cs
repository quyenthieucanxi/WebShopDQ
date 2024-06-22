

using Microsoft.EntityFrameworkCore.Infrastructure;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using WebShopDQ.App.Common.Constant;

namespace WebShopDQ.App.Models
{
    public class Order : BaseModel
    {
        public Order() { }
        public Order(Guid id) : base(id)
        {
        }

        public Guid UserID { get; set; }
        public Guid ProductID { get; set; }
        public Guid AddressShippingID { get; set; }
        public int Quantity { get; set; }
        public float TotalPrice { get; set; }
        public string? Status {  get; set; } = OrderStatus.Pending;
        public string Payment { get; set; } = null!;
        public string? Note { get; set; }
        public User? UserOrder { get; set; }
        public AddressShipping AddressShipping { get; set; } = null!;
        public ICollection<Post>? Products { get; set; } 
    }
}
