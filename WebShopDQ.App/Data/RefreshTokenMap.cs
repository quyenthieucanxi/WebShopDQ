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
    public class RefreshTokenMap : IEntityTypeConfiguration<RefreshToken>
    {
        public void Configure(EntityTypeBuilder<RefreshToken> builder)
        {
            builder.HasOne(p => p.User)
                    .WithMany()
                    .HasForeignKey(p => p.UserId)
                    .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
