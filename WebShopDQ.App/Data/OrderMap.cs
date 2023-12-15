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
    public class OrderMap : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasOne(p => p.UserOrder)
                    .WithMany(q => q.Orders)
                    .HasForeignKey(p => p.UserID)
                    .OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(p => p.Product)
                    .WithOne()
                    .HasForeignKey<Order>(p => p.ProductID)
                    .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
