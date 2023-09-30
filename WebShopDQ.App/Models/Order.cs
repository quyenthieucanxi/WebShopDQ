

namespace WebShopDQ.App.Models
{
    public class Order :BaseModel
    {
        public Guid UserID { get; set; }
        public Guid ProductID { get; set; }
        public int Quantity { get; set; }
        public float TotalPrice { get; set; }
        public User? User { get; set; }
        public Post? Product { get; set; }
    }
}
