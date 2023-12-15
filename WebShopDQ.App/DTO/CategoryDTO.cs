﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShopDQ.App.DTO
{
    public class CategoryDTO
    {
        [Required(ErrorMessage = "Category Name is required")]
        public string? CategoryName { get; set; }
        [Required(ErrorMessage = "CategoryPath is required")]
        public string? CategoryPath { get; set; }
        
        public string? urlImg { get; set; }
    }
}
