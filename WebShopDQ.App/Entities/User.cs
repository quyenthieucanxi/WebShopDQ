using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebShopDQ.App.Models
{
    public class User : IdentityUser<Guid>
    {
        [Required]
        public string? FullName { get; set; }
        public string? Address { get; set; }
        public string? Introduce { get; set; }
        [Required]
        public string? Gender { get; set; }
        public DateTime Dob { get; set; }
        public string? AvatarUrl { get; set; }
        public bool IsActive { get; set; }

        [NotMapped]
        public virtual List<Order>? Orders { get; set; }
        [NotMapped]
        public virtual List<Message>? SentMessages { get; set; }
        [NotMapped]
        public virtual List<Message>? ReceivedMessages { get; set; }
        [NotMapped]
        public virtual List<Notify>? Notifies { get; set; }
    }
}
