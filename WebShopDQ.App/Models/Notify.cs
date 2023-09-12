

namespace WebShopDQ.App.Models
{
    public class Notify
    {
        public Guid ID { get; set; }
        public Guid UserID { get; set; }
        public string? Content { get; set; }
        public bool IsRead { get; set; }
        public string? TypeNotify { get; set; }
        public DateTime Created { get; set; }

        public User? User { get; set; }
    }
}
