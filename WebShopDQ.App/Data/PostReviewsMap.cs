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
    public class PostReviewsMap : IEntityTypeConfiguration<PostReviews>
    {
        public void Configure(EntityTypeBuilder<PostReviews> builder)
        {
            builder.Property(p => p.ReviewText).HasMaxLength(256);
            builder.HasOne(p => p.User)
                    .WithMany()
                    .HasForeignKey(p => p.UserId)
                    .OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(p => p.Post)
                    .WithMany()
                    .HasForeignKey(p => p.PostId)
                    .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
