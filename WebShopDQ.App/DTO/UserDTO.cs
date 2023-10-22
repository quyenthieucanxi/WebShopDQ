using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShopDQ.App.DTO
{
    public class UserDTO
    {
        public string? Email { get; set; }
        public string? FullName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
        public string? Introduce { get; set; }
        public string? Gender { get; set; }
        public DateTime Dob { get; set; }
        public string? PublicIdAvatar { get; set; }
        public string? UrlAvatar { get; set; }
    }

    public class UserInfoDTO
    {
        public string? Email { get; set; }
        public string? FullName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
        public string? Introduce { get; set; }
        public string? Gender { get; set; }
        public DateTime Dob { get; set; }
    }
}
