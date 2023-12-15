

using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace WebShopDQ.App.Models
{
    public class Order : BaseModel
    {
        public Guid UserID { get; set; }
        public Guid ProductID { get; set; }
        public Guid AddressShippingID { get; set; }
        public int Quantity { get; set; }
        public float TotalPrice { get; set; }
        public string? Status {  get; set; } = "Chờ xác nhận";
        public string Payment { get; set; } = null!;
        public string? Note { get; set; }
        public User? UserOrder { get; set; }
        public AddressShipping AddressShipping { get; set; } = null!;
        public Post? Product { get; set; }
    }
}
