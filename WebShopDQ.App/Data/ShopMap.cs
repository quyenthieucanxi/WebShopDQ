using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShopDQ.App.Models;

namespace WebShopDQ.App.Data
{
    public class ShopMap : IEntityTypeConfiguration<Shop>
    {
        public void Configure(EntityTypeBuilder<Shop> builder)
        {
            builder.Property(p => p.Name).HasMaxLength(50);
            builder.Property(p => p.Path).HasMaxLength(100);
            builder.Property(p => p.Description).HasMaxLength(1500);
            builder.Property(p => p.Address).HasMaxLength(150);
            builder.HasOne(p => p.User)
                .WithOne(q => q.Shop)
                .HasForeignKey<Shop>(p => p.UserId )
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
