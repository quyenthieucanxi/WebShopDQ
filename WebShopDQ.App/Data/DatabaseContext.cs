using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebShopDQ.App.Models;

namespace WebShopDQ.App.Data
{
    public class DatabaseContext : IdentityDbContext<IdentityUser<Guid>, IdentityRole<Guid>, Guid>
    {
        public DatabaseContext() { }
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base (options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<Roles> Roles { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Bỏ tiền tố AspNet của các bảng: mặc định
            foreach (var entityType in builder.Model.GetEntityTypes())
            {
                var tableName = entityType.GetTableName();
                if (!string.IsNullOrEmpty(tableName) && tableName.StartsWith("AspNet"))
                {
                    entityType.SetTableName(tableName.Substring(6));
                }
            }
            builder.ApplyConfiguration(new UserMap());
            Seeding.SeedRoles(builder);
        }
    }
}
