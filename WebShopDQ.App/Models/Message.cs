

using System.ComponentModel.DataAnnotations;

namespace WebShopDQ.App.Models
{
    public class Message : BaseModel
    {
        public Message() { }
        public Message(Guid id) : base(id)
        {
        }


        public Guid ChatId { get; set; }
        public Guid UserId { get; set; }
        public string? Content { get; set; }
        public virtual Chats? Chats { get; set; }
        public virtual User? User { get; set; }      
    }
}
