

using System.ComponentModel.DataAnnotations;

namespace WebShopDQ.App.Models
{
    public class Category
    {
        public Guid Id { get; set; }

        [Required]
        public string? CategoryName { get; set; }

        public  List<Product>? Products { get; set; }
    }
}
