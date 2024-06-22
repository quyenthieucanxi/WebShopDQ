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
    public class OrderDetailMap: IEntityTypeConfiguration<OrderDetail>
    {
        public void Configure(EntityTypeBuilder<OrderDetail> builder)
        {
            builder.HasOne(o => o.Post)
                    .WithOne()
                    .HasForeignKey<OrderDetail>(o => o.ProductId)
                    .OnDelete(DeleteBehavior.NoAction);
            builder.HasMany<OrderDetail>()
                    .WithOne()
                    .HasForeignKey(o => o.OrderId)
                    .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
