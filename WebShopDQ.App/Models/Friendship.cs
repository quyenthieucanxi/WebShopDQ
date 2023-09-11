

namespace WebShopDQ.App.Models
{
    public class Friendship
    {
        public Guid Id { get; set; }
        public Guid FollowerID { get; set; }
        public Guid FollowingID { get; set; }

/*        public User Follower { get; set; }
        public User Following { get; set; }*/
    }
}
