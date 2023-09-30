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
    public class ChatsMap : IEntityTypeConfiguration<Chats>
    {
        public void Configure(EntityTypeBuilder<Chats> builder)
        {
            builder.HasIndex(p => new { p.SenderID, p.ReceiverID });
            builder.HasOne(p => p.Sender)
                    .WithMany()
                    .HasForeignKey(p => p.SenderID)
                    .OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(p => p.Receiver)
                    .WithMany()
                    .HasForeignKey(p => p.ReceiverID)
                    .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
