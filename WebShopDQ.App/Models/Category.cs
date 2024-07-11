

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebShopDQ.App.Models
{
    public class Category : BaseModel
    {
        public Category() { }
        public Category(Guid id) : base(id)
        {
        }

        public Category(Guid id, string? categoryName, string? categoryPath, string? urlImg) : base(id)
        {
            CategoryName = categoryName;
            CategoryPath = categoryPath;
            this.urlImg = urlImg;
        }

        [Required]
        public string? CategoryName { get; set; }
        [Required]
        public string? CategoryPath { get; set; }
        public string? urlImg  { get; set; }
        public virtual List<Post>? Posts { get; set; }
        public bool IsDelete { get; set; } = false;
    }
}
