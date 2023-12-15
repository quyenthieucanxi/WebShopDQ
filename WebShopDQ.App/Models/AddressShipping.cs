using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShopDQ.App.Models
{
    public class AddressShipping : BaseModel
    {
        public Guid UserId { get; set; }

        public string RecipientName { get; set; } = null!;

        public string Phone { get; set; } = null!;

        public string Province { get; set; } = null!;

        public string Distrist { get; set; } = null!;

        public string AddressDetail { get; set; } = null!;

        public bool IsDefault { get; set; } = false;

        public User User { get; set; } = null!;

        public List<Order>? Orders { get; set; } 

    }
}
