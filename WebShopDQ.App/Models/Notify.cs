

namespace WebShopDQ.App.Models
{
    public class Notify : BaseModel
    {
        public Notify(Guid id) : base(id)
        {
        }

        public Guid UserID { get; set; }
        public string? NotifyText { get; set; }
        public bool IsRead { get; set; }
        public string? TypeNotify { get; set; }
        public User? User { get; set; }
    }
}
