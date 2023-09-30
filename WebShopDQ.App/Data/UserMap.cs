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
    public class UserMap : IEntityTypeConfiguration<User>
    {

        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(p => p.FullName).HasMaxLength(50);
            builder.Property(p => p.Address).HasMaxLength(150);
            builder.Property(p => p.Introduce).HasMaxLength(256);
            builder.Property(p => p.AvatarUrl).HasMaxLength(1000);
        }
    }
}
