using System;
using System.ComponentModel.DataAnnotations;

namespace WebShopDQ.App.Models.Authentication
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "UserName is required")]
        public string? UserName { get; set; }

        [EmailAddress]
        [Required(ErrorMessage = "Email is required")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string? Password { get; set; }
    }
}
