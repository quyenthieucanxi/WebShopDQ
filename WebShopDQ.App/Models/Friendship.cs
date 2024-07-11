

namespace WebShopDQ.App.Models
{
    public class Friendship : BaseModel
    {
        public Friendship(Guid id) : base(id)
        {
        }

        public Friendship(Guid id, Guid followerID, Guid followingID) : base(id)
        {
            FollowerID = followerID;
            FollowingID = followingID;
        }

        public Guid FollowerID { get; set; }
        public Guid FollowingID { get; set; }
        public virtual User Follower { get; set; } = null!;
        public virtual User Following { get; set; } = null!;
    }
}
