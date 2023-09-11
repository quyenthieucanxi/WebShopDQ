using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebShopDQ.App.Models;

namespace WebShopDQ.App.Data
{
    public class DatabaseContext : IdentityDbContext<IdentityUser>
    {

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base (options)
        { 

        }

        public DbSet<User> User { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {

            base.OnModelCreating(builder);
            // Bỏ tiền tố AspNet của các bảng: mặc định
            foreach (var entityType in builder.Model.GetEntityTypes())
            {
                var tableName = entityType.GetTableName();
                if (tableName.StartsWith("AspNet"))
                {
                    entityType.SetTableName(tableName.Substring(6));
                }
            }
            builder.Entity<User>().ToTable("useracount");
        }


    }
}
