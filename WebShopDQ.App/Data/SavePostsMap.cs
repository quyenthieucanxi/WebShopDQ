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
    public class SavePostsMap : IEntityTypeConfiguration<SavePosts>
    {
        public void Configure(EntityTypeBuilder<SavePosts> builder)

        {
            builder.HasKey(sp => new { sp.UserID, sp.PostID });
            builder.HasOne(p => p.User)
                    .WithMany(q => q.SavePosts)
                    .HasForeignKey(p => p.UserID)
                    .OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(p => p.Post)
                    .WithMany()
                    .HasForeignKey(p => p.PostID)
                    .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
