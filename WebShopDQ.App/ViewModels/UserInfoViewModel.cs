using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShopDQ.App.ViewModels
{
    public class UserInfoViewModel : BaseVM
    {
        public string? Email { get; set; }
        public string? FullName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
        public string? Introduce { get; set; }
        public string? Gender { get; set; }
        public DateTime Dob { get; set; }
        public string? Url { get; set; }
        public string? AvatarUrl { get; set; }
        public bool IsActive { get; set; } = true;
        public string? Role { get; set; }
        public int FollowedCount { get; set; }
        public int FollowingCount { get; set; }
    }

    public class UserListViewModel
    {
        public int TotalUser { get; set; }
        public ICollection<UserInfoViewModel>? UserList { get; set; }
    }
}
