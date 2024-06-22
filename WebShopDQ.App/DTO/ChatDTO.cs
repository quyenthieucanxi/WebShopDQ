using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShopDQ.App.DTO
{
    public class ChatDTO
    {
        public string urlSender { get; set; } = null!;
        public string urlReceiver { get; set; } = null!;
        public string message { get; set; } = null!;
    }
}
