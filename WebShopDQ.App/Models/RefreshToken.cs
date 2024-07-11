using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebShopDQ.App.Models
{
    public class RefreshToken : BaseModel
    {
        public RefreshToken(Guid id) : base (id)
        {
        }

        public RefreshToken(Guid Id, Guid userId, User? user, string? token, 
            string? jwtId, bool isUsed, bool isRevoked, 
            DateTime issuedAt, DateTime expiredAt) : base(Id)
        {
            UserId = userId;
            User = user;
            Token = token;
            JwtId = jwtId;
            IsUsed = isUsed;
            IsRevoked = isRevoked;
            IssuedAt = issuedAt;
            ExpiredAt = expiredAt;
        }

        public Guid UserId { get; set; }
        public User? User { get; set; }
        public string? Token { get; set; }
        public string? JwtId { get; set; }
        public bool IsUsed { get; set; }
        public bool IsRevoked { get; set; }
        public DateTime IssuedAt { get; set; }
        public DateTime ExpiredAt { get; set; }
    }
}
