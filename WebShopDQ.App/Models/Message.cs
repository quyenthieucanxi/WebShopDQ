

using System.ComponentModel.DataAnnotations;

namespace WebShopDQ.App.Models
{
    public class Message : BaseModel
    {
        public Guid ChatId { get; set; }
        public Guid UserId { get; set; }
        public string? Content { get; set; }
        public virtual Chats? Chats { get; set; }
        public virtual User? User { get; set; }      
    }
}
