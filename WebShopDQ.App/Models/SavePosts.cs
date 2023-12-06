using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
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
        public virtual User User { get; set; } = null!;
        public virtual Post Post { get; set; } = null!;
    }
}
