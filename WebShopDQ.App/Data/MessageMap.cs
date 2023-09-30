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
    public class MessageMap : IEntityTypeConfiguration<Message>
    {
        public void Configure(EntityTypeBuilder<Message> builder)
        {
            builder.Property(p => p.Content).HasMaxLength(1000);
            builder.HasOne(p => p.User)
                    .WithMany(q => q.Messages)
                    .HasForeignKey(p => p.UserId)
                    .OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(p => p.Chats)
                    .WithOne()
                    .HasForeignKey<Message>(p => p.ChatId)
                    .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
