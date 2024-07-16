using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShopDQ.App.DTO
{
    public class PostDTO
    {
        public Guid CategoryId { get; set; }
        [Required]
        public string? Title { get; set; }
        [Required]
        public string? PostPath { get; set; }
        [Required]
        public string? Description { get; set; }
        [Required]
        public string? UrlImage { get; set; }
        public string? UrlImage1 { get; set; }
        public string? UrlImage2 { get; set; }
        public double Price { get; set; }
        [Required]
        public string? Address { get; set; }
        public int Quantity { get; set; }
    }

    public class UpdatePostDTO
    {
        public Guid Id { get; set; }
        [Required]
        public string? Title { get; set; }
        [Required]
        public string? PostPath { get; set; }
        [Required]
        public string? Description { get; set; }
        [Required]
        public string? UrlImage { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
    }
}
