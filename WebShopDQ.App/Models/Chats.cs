using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShopDQ.App.Models
{
    public class Chats : BaseModel
    {
        public Guid SenderID { get; set; }
        public Guid ReceiverID { get; set; }
        public User Sender { get; set; } = null!;
        public User Receiver { get; set; } = null!;
    }
}
