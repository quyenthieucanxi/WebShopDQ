using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShopDQ.App.Models;

namespace WebShopDQ.App.ViewModels
{
    public class ChatViewModel : BaseVM
    {
        public string Messages { get; set; } = null!;
        public bool isSenderRead { get; set; }
        public bool isReceiverRead { get; set; }
        public UserInfoViewModel Receiver { get; set; } = null!;
        public UserInfoViewModel Sender { get; set; } = null!;
    }
    public class GroupChatByTime : BaseVM
    {
        public string time { get; set; } = null!;
        public ICollection<ChatViewModel>? messages { get; set; } 
    }
}
