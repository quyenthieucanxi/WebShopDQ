using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShopDQ.App.ViewModels
{
    public class TokenViewModel
    {
        public Guid UserId { get; set; }
        public object? CurrentUser { get; set; }
    }

    public class DecodeModel
    {
        public string? UserId { get; set; }
    }
}
