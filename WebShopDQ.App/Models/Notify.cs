

namespace WebShopDQ.App.Models
{
    public class Notify : BaseModel
    {
        public Notify() { }
        public Notify(Guid id) : base(id)
        {
        }

        public Guid UserIdReceiver { get; set; }
        public Guid UserIdSender { get; set; }
        public string? NotifyText { get; set; }
        public bool IsRead { get; set; }
        public string? TypeNotify { get; set; }
        public User? UserReceiver { get; set; }
        public User? UserSender { get; set; }
    }
}
