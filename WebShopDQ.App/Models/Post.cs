

namespace WebShopDQ.App.Models
{
    public class Post
    {
        public Guid Id { get; set; }
        public Guid UserID { get; set; }
        public Guid CategoryID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Created { get; set; }

/*        public User User { get; set; }
        public Category Category { get; set; }*/
    }
}
