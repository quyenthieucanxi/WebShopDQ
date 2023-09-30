

namespace WebShopDQ.App.Models
{
    public class Friendship : BaseModel
    {
        public Guid FollowerID { get; set; }
        public Guid FollowingID { get; set; }
        public virtual User Follower { get; set; } = null!;
        public virtual User Following { get; set; } = null!;
    }
}
