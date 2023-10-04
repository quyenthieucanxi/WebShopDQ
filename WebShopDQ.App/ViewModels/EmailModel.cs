
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShopDQ.App.ViewModels
{
    public class EmailModel
    {

    }

    public class LinkedEmailModel
    {
        public string? Email { get; set; }
        public string? Link { get; set; }
    }

    public class EmailMessageModel
    {
        public string? To { get; set; }
        public string? Subject { get; set; }
        public string? Body { get; set; }
    }
}
