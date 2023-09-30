using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WebShopDQ.App.Models;

namespace WebShopDQ.App.Data
{
    public class DatabaseContext : IdentityDbContext<IdentityUser<Guid>, IdentityRole<Guid>,Guid>
    {
        public DatabaseContext() { }
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {

        }
        public DbSet<User> User { get; set; } = null!;
        public DbSet<UserToken> UserToken { get;set; } = null!; 
        public DbSet<Role> Role { get; set; } = null!;
        public DbSet<Category> Categories { get; set; } = null!;
        public DbSet<Chats> Chats { get; set; } = null!;
        public DbSet<Order> Orders { get; set; } = null!;
        public DbSet<Post> Posts { get; set; } = null!;
        public DbSet<PostReviews> PostReviews { get; set; } = null!;
        public DbSet<Notify> Notifies { get; set; } = null!; 
        public DbSet<Message> Message { get; set; } = null!;
        public DbSet<SavePosts> SavePosts { get; set; } = null!;
        public DbSet<Friendship> Friendships { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            //  Detele AspNet of table
            foreach (var entityType in builder.Model.GetEntityTypes())
            {
                var tableName = entityType.GetTableName();
                if (!string.IsNullOrEmpty(tableName) && tableName.StartsWith("AspNet"))
                {
                    entityType.SetTableName(tableName.Substring(6));
                }
            }
            builder.ApplyConfiguration(new UserMap());
            builder.ApplyConfiguration(new UserTokenMap());
            builder.ApplyConfiguration(new PostMap());
            builder.ApplyConfiguration(new CategoryMap());
            builder.ApplyConfiguration(new OrderMap());
            builder.ApplyConfiguration(new MessageMap());
            builder.ApplyConfiguration(new FriendShipMap());
            builder.ApplyConfiguration(new NotifyMap());
            builder.ApplyConfiguration(new PostReviewsMap());
            builder.ApplyConfiguration(new SavePostsMap());
            builder.ApplyConfiguration(new ChatsMap());
        }
    }
}
