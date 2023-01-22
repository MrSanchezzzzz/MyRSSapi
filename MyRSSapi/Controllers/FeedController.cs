using Microsoft.AspNetCore.Mvc;

namespace MyRSSapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FeedController : ControllerBase
    {


        private readonly ILogger<FeedController> _logger;

        public FeedController(ILogger<FeedController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetAllFeed")]
        public IEnumerable<Feed> Get()
        {
            List<Feed> list = new List<Feed>();
            using(FeedContext feedContext = new FeedContext())
            {
                list = feedContext.Feeds.ToList();
            }
            return list;
        }

        [HttpGet(Name = "GetFeed")]
        public Feed GetFeedById(int id)
        {
            Feed feed = new Feed();
            using (FeedContext feedContext = new FeedContext())
            {
                feed = feedContext.Feeds.Where(f=>f.Id==id).First();
            }
            return feed;
        }

        [HttpGet(Name = "GetFeedFromDate")]
        public Feed GetFromDate(DateOnly date)
        {
            Feed feed = new Feed();
            using (FeedContext feedContext = new FeedContext())
            {
                feed = feedContext.Feeds.Where(f => f.Date.CompareTo(date)==1&&!f.Read).First();
            }
            return feed;
        }

        [HttpPost(Name = "PostFeed")]
        public void Post(Feed feed)
        {
            using(FeedContext feedContext = new FeedContext())
            {
                feedContext.Feeds.Add(feed);
                feedContext.SaveChanges();
            }

        }

        [HttpPut(Name = "PutFeed")]
        public void Post(int id,Feed feed)
        {
            using (FeedContext feedContext = new FeedContext())
            {
                Feed newFeed = feedContext.Feeds.Where(f => f.Id == id).First();
                newFeed.Date = feed.Date;
                newFeed.Url = feed.Url;
                newFeed.Read = feed.Read;
                feedContext.SaveChanges();
            }

        }
        [HttpPut(Name = "MarkAsRead")]
        public void MarkAsRead(int id)
        {
            using (FeedContext feedContext = new FeedContext())
            {
                Feed newFeed = feedContext.Feeds.Where(f => f.Id == id).First();
                newFeed.Read = true;
                feedContext.SaveChanges();
            }

        }

        [HttpDelete(Name ="DeleteFeed")]
        public void Delete(int id)
        {
            using(FeedContext feedContext = new FeedContext())
            {
                feedContext.Entry(new Feed { Id = id }).State = System.Data.Entity.EntityState.Deleted;
                feedContext.SaveChanges();
            }
        }
    }
}