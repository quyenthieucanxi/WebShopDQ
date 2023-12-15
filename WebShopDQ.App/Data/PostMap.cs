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
    public class PostMap : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.Property(p => p.Title).HasMaxLength(50);
            builder.Property(p => p.PostPath).HasMaxLength(50);
            builder.Property(p => p.Description).HasMaxLength(1500);
            builder.Property(p => p.UrlImage).HasMaxLength(1000);
            builder.Property(p => p.Address).HasMaxLength(150);
            builder.HasOne(p=> p.User)
                .WithMany(q => q.ListPost)
                .HasForeignKey(p=> p.UserID)
                .IsRequired(true)
                .OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(p => p.Category)
                .WithMany(q => q.Posts)
                .HasForeignKey(p => p.CategoryID)
                .OnDelete(DeleteBehavior.NoAction);

        }
    }
}
