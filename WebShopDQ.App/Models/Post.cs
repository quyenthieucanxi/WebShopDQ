using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebShopDQ.App.Models
{
    public class Post : BaseModel
    {
        public Post() { }
        public Post(Guid id) : base(id)
        {
        }

        public Post(Guid id, Guid userID, Guid categoryID, string title,
                    string postPath, string? description, string? urlImage, 
                    double price, string? address, int quantity) : base(id)
        {
            UserID = userID;
            CategoryID = categoryID;
            Title = title;
            PostPath = postPath;
            Description = description;
            UrlImage = urlImage;
            Price = price;
            Address = address;
            Quantity = quantity;
        }

        public Guid UserID { get; set; }
        public Guid CategoryID { get; set; }
        public Guid? OrderId { get; set; }
        [Required]
        public string Title { get; set; } = null!;
        [Required]
        public string PostPath { get; set; } = null!;
        [Required]
        public string? Description { get; set; }
        [Required]
        public string? UrlImage { get; set; }
        public double Price { get; set; }
        [Required]
        public string? Address { get; set; }
        public bool IsDelete { get; set; } = false;
        public bool IsTrend { get; set; } = false;
        public string? requestTrend { get; set; }
        public string Status { get; set; } = "Chờ duyệt";
        [Required]
        public int Quantity { get; set; }
        [NotMapped]
        public  User? User { get; set; }
        [NotMapped]
        public  Category? Category { get; set; }
        [NotMapped]
        public Order? Order { get; set; }
        public List<Files>? Files { get; set; } 
    }
}
