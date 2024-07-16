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
    public class FilesMap : IEntityTypeConfiguration<Files>
    {
        public void Configure(EntityTypeBuilder<Files> builder)
        {
            builder.HasOne(p => p.Product)
                .WithMany(q => q.Files)
                .HasForeignKey(p =>  p.productID)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
