﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShopDQ.App.Models
{
    public class BaseModel
    {
        public BaseModel ()
        {

        }
        public BaseModel(Guid id)
        {
            Id = id;
        }
        public Guid Id { get; set; }
        public DateTime CreatedTime { get; set; } = DateTime.UtcNow;
        public DateTime ModifiedTime { get; set; } = DateTime.UtcNow;
    }
}
