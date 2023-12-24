using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShopDQ.App.Models
{
    public class Shop : BaseModel
    {
        public string Name { get; set; } = null!;
        public string Path { get; set; }  = null!;
        public string Description { get; set; } = null!;
        public string Address { get; set; } = null!;
        public Guid UserId { get; set; }
        public User? User { get; set; }
    }
}
