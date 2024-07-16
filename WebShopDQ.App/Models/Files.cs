using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShopDQ.App.Models
{
    public class Files : BaseModel
    {
        public Files()
        {
        }

        public Files(Guid id) : base(id)
        {
        }
        public string url { get; set; } = null!;
        public Guid productID { get; set; }
        public Post? Product { get; set; } 
    }
}
