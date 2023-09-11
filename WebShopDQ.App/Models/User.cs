using Microsoft.AspNetCore.Identity;

namespace WebShopDQ.App.Models
{
    public class User : IdentityUser<Guid>
    {
        public string FullName { get; set; }
        public string Address { get; set; }
        public string Introduce { get; set; }
        public string Gender { get; set; }
        public DateTime Dob { get; set; }
        public string AvatarUrl { get; set; }
        public bool IsActive { get; set; }

/*        public List<Order> Orders { get; set; }
        public List<Message> SentMessages { get; set; }
        public List<Message> ReceivedMessages { get; set; }
        public List<Notify> Notifies { get; set; }*/
    }
}
