using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShopDQ.App.Models;

namespace WebShopDQ.App.Interceptor
{
    public class UpdateAuditableEntitiesInterceptor : SaveChangesInterceptor
    {
        public override ValueTask<int> SavedChangesAsync(
            SaveChangesCompletedEventData eventData, int result, 
            CancellationToken cancellationToken = default)
        {
            DbContext? dbContext = eventData.Context;
            if (dbContext is null)
            {
                return base.SavedChangesAsync(eventData, result, cancellationToken);
            }
            IEnumerable<EntityEntry<BaseModel>> entries = dbContext.ChangeTracker.Entries<BaseModel>();

            foreach (EntityEntry<BaseModel> entry in entries)
            {
                if (entry.State == EntityState.Modified)
                {
                    entry.Property(p => p.ModifiedTime).CurrentValue = DateTime.UtcNow;
                }
            }

            return base.SavedChangesAsync(eventData, result, cancellationToken);
        }
    }
}
