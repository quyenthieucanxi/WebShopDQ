using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShopDQ.App.Models;

namespace WebShopDQ.App.ViewModels
{
    public class NotifyViewModel : BaseVM
    {
        public string? NotifyText { get; set; }
        public bool IsRead { get; set; }
        public string? TypeNotify { get; set; }
        public UserInfoViewModel? UserSender { get; set; }
    }
}
