

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebShopDQ.App.Models
{
    public class Product
    {
        public Guid ID { get; set; }
        public Guid CategoryID { get; set; }
        public Guid SellerID { get; set; }

        [Required]
        public string? ProductName { get; set; }
        public string? Description { get; set; }
        public float Price { get; set; }
        public string? ImageURL { get; set; }

/*        public Category Category { get; set; }
        public User Seller { get; set; }*/
    }
}
