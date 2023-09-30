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
                    .WithMany()
                    .HasForeignKey(p => p.FollowerID)
                    .OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(p => p.Following)
                    .WithMany()
                    .HasForeignKey(p => p.FollowingID)
                    .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
