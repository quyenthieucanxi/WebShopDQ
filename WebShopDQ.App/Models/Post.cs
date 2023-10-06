using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebShopDQ.App.Models
{
    public class Post : BaseModel
    { 
        public Guid UserID { get; set; }
        public Guid CategoryID { get; set; }
        [Required]
        public string? Title { get; set; }
        [Required]
        public string? Description { get; set; }
        [Required]
        public string? UrlImage { get; set; }
        public double Price { get; set; }
        [Required]
        public string? Address { get; set; }
        public bool IsDelete { get; set; } = false;
        public string Status { get; set; } = "Chờ duyệt";
        [NotMapped]
        public User? User { get; set; }
        [NotMapped]
        public Category? Category { get; set; }
    }
}
