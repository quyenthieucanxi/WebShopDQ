using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShopDQ.App.Models
{
    public class Chats : BaseModel
    {
        public Chats() { }
        public Chats(Guid id) : base(id)
        {
        }
        public Chats(Guid id,
            Guid senderID,
            Guid receiverID,
            string messages,
            bool isSenderRead ,
            bool isReceiverRead) : base(id)
        {
            SenderID = senderID;
            ReceiverID = receiverID;
            Messages = messages;
            this.isSenderRead = isSenderRead;
            this.isReceiverRead = isReceiverRead;
        }

        public Guid SenderID { get; set; }
        public Guid ReceiverID { get; set; }
        public string Messages { get; set; } = null!;
        public bool isSenderRead { get; set; } 
        public bool isReceiverRead { get; set; } 
        public User Sender { get; set; } = null!;
        public User Receiver { get; set; } = null!;
    }
}
