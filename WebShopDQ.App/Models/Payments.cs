using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShopDQ.App.Models
{
    public class Payments : BaseModel
    {
        public Guid OrderId { get; set; }
        public DateTime PaymentDate { get; set; }
        
    }
}
