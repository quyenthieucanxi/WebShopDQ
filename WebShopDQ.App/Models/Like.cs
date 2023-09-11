using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShopDQ.App.Models
{
    public class Like
    {
        public Guid Id { get; set; }
        public Guid UserID { get; set; }
        public Guid PostID { get; set; }

/*        public User User { get; set; }
        public Post Post { get; set; }*/
    }
}
