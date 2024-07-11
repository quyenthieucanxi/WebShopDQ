using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShopDQ.App.Models;

namespace WebShopDQ.App.Data
{
    public class FriendShipMap : IEntityTypeConfiguration<Friendship>
    {
        public void Configure(EntityTypeBuilder<Friendship> builder)
        {
            builder.HasKey(p => new { p.FollowingID, p.FollowerID });
            builder.HasOne(p => p.Follower)
                    .WithMany(u => u.Followings)
                    .HasForeignKey(p => p.FollowerID)
                    .OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(p => p.Following)
                    .WithMany(u => u.Followers)
                    .HasForeignKey(p => p.FollowingID)
                    .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
