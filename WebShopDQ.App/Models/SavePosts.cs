using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShopDQ.App.Models
{
    public class SavePosts : BaseModel
    {
        public Guid UserID { get; set; }
        public Guid PostID { get; set; }
        public User User { get; set; } = null!;
        public Post Post { get; set; } = null!;
    }
}
