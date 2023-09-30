﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShopDQ.App.Models
{
    public class PostReviews : BaseModel
    {
        public Guid UserId { get; set; }
        public Guid PostId { get; set; }
        public int Rating { get; set; } 
        public string? ReviewText { get; set; }
        public User? User { get; set; }
        public Post? Post { get; set; }
     }
}
