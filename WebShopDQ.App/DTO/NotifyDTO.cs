using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShopDQ.App.DTO
{
    public class NotifyDTO
    {
        public Guid UserID { get; set; }
        public string? NotifyText { get; set; }
        public bool IsRead { get; set; }
        public string? TypeNotify { get; set; }
    }
}
