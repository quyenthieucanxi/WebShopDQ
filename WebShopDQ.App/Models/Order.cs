

namespace WebShopDQ.App.Models
{
    public class Order
    {
        public Guid Id { get; set; }
        public Guid UserID { get; set; }
        public Guid ProductID { get; set; }
        public int Quantity { get; set; }
        public float TotalPrice { get; set; }
        public DateTime Created { get; set; }

/*        public User User { get; set; }
        public Product Product { get; set; }*/
    }
}
