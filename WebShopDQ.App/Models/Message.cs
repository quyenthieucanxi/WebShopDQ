

using System.ComponentModel.DataAnnotations;

namespace WebShopDQ.App.Models
{
    public class Message
    {
        public Guid Id { get; set; }
        public Guid SenderID { get; set; }
        public Guid ReceiverID { get; set; }
        public string Content { get; set; }
        public DateTime Created { get; set; }

/*        public User Sender { get; set; }
        public User Receiver { get; set; }*/
    }
}
