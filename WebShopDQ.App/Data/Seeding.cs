using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebShopDQ.App.Models;

namespace WebShopDQ.App.Data
{
    public static class Seeding
    {
        // Seed roles
        public static void SeedRoles(ModelBuilder builder)
        {
            builder.Entity<Role>().HasData
                (
                    new Role() { Id = Guid.NewGuid(), Name = "Admin", ConcurrencyStamp = "1", NormalizedName = "ADMIN" },
                    new Role() { Id = Guid.NewGuid(), Name = "Manager", ConcurrencyStamp = "2", NormalizedName = "MANAGER" },
                    new Role() { Id = Guid.NewGuid(), Name = "Shiper", ConcurrencyStamp = "3", NormalizedName = "SHIPER" },
                    new Role() { Id = Guid.NewGuid(), Name = "Seller", ConcurrencyStamp = "4", NormalizedName = "SELLER" },
                    new Role() { Id = Guid.NewGuid(), Name = "User", ConcurrencyStamp = "5", NormalizedName = "USER" }
                );
        }
    }
}
