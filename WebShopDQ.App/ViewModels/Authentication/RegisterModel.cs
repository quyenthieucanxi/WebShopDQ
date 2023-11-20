using System;
using System.ComponentModel.DataAnnotations;

namespace WebShopDQ.App.ViewModels.Authentication
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "UserName is required")]
        public string? UserName { get; set; }

        [EmailAddress]
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "Password is required")]
        public string? Password { get; set; }

        public string? FullName { get; set; }

        public string? Image { get; set; }
    }
}
