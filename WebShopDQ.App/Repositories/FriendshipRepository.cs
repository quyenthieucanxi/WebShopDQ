using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebShopDQ.App.Data;
using WebShopDQ.App.Models;
using WebShopDQ.App.Repositories.IRepositories;

namespace WebShopDQ.App.Repositories
{
    public class FriendshipRepository : Repository<Friendship>, IFriendshipRepository
    {
        public FriendshipRepository(DatabaseContext databaseContext) : base(databaseContext)
        {
            
        }

        public async Task<ICollection<Friendship>> GetFollowers(Guid userId)
        {
            var followers = await FindAllAsync(f => f.FollowerID == userId);
            return followers.ToList();
        }
    }
}
