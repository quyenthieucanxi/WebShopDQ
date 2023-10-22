using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        public bool IsActive { get; set; } = true;
        public DateTime CreatedTime { get; set; } = DateTime.Now;
        public DateTime ModifiedTime { get; set; } = DateTime.Now;
        public string? Url { get; set; }
        public string? PublicId {  get; set; } 
        public virtual List<Post>? Posts { get; set; }
        public virtual List<SavePosts>? SavePosts { get; set; }
        public virtual List<Message>? Messages { get; set; }
        public virtual List<Notify>? Notifies { get; set; }
    }
}
