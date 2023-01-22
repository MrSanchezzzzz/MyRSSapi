using System.Data.Entity;

namespace MyRSSapi
{
    public class Feed
    {
        public int Id { get; set; }
        public DateOnly Date { get; set; }
        public string Url {get; set; }
        public bool Read { get; set; }

    }
    public class FeedContext : DbContext
    {
        public DbSet<Feed> Feeds { get; set;}
    }
}