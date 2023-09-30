

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebShopDQ.App.Models
{
    public class Category : BaseModel
    {
        [Required]
        public string? CategoryName { get; set; }
        public List<Post>? Posts { get; set; }
    }
}
