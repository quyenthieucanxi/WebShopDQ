

using System.ComponentModel.DataAnnotations;

namespace WebShopDQ.App.Models
{
    public class Order : BaseModel
    {
        public Guid UserId { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public float TotalPrice { get; set; }
        public string? Status {  get; set; } = "Đang xử lý.";
        [Required]
        public string? Address { get; set; }
        [Required]
        public string? PhoneNumber { get; set; }
        public string? Note { get; set; }
        public User? User { get; set; }
        public Post? Product { get; set; }
    }
}
