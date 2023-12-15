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
    public class AddressShippingMap :  IEntityTypeConfiguration<AddressShipping>
    {
        public void Configure(EntityTypeBuilder<AddressShipping> builder)
        {
            builder.HasOne(p => p.User)
                .WithMany(q => q.AddressShippings)
                .HasForeignKey(p => p.UserId)
                .OnDelete(DeleteBehavior.NoAction);
            builder.HasMany(a => a.Orders)
                .WithOne(o => o.AddressShipping)
                .HasForeignKey(o => o.AddressShippingID)
                .OnDelete(DeleteBehavior.NoAction);

        }
    }
}
