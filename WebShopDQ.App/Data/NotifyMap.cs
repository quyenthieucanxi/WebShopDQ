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
    public class NotifyMap : IEntityTypeConfiguration<Notify>
    {
        public void Configure(EntityTypeBuilder<Notify> builder)
        {
            builder.Property(p => p.NotifyText).HasMaxLength(256);
            builder.Property(p => p.TypeNotify).HasMaxLength(50);
            builder.HasOne(p => p.UserSender)
                    .WithMany(q => q.Notifies)
                    .HasForeignKey(p => p.UserIdSender)
                    .OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(p => p.UserReceiver)
                    .WithMany()
                    .HasForeignKey(p => p.UserIdReceiver)
                    .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
