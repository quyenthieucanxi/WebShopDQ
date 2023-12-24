using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace WebShopDQ.App.Models
{
    public class User : IdentityUser<Guid>
    {
        [Required]
        public string? FullName { get; set; }
        [Required]
        public string? Address { get; set; }
        public string? Introduce { get; set; }
        [Required]
        public string? Gender { get; set; }
        public DateTime Dob { get; set; }
        public string? AvatarUrl { get; set; }
        public bool IsActive { get; set; } = false;
        public DateTime CreatedTime { get; set; } = DateTime.Now;
        public DateTime ModifiedTime { get; set; } = DateTime.Now;
        public string? Url { get; set; }
        public string? PublicId {  get; set; } 
        public Shop? Shop { get; set; }
        public  List<Post>? ListPost { get; set; }
        public  List<Order>? Orders { get; set; }
        public  List<AddressShipping>? AddressShippings { get; set; }
        public  List<SavePosts>? SavePosts { get; set; }
        public  List<Message>? Messages { get; set; }
        public  List<Notify>? Notifies { get; set; }
    }
}
