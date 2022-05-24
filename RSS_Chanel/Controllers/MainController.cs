using Microsoft.AspNetCore.Mvc;
using RSS_Chanel.Models;
using RSS_Chanel.Utilities;

namespace RSS_Chanel.Controllers
{
    public class MainController : Controller
    {
        List<RssFeed> feeds = new List<RssFeed>();
        private static List<RssFeed> getFeeds()
        {
            var tempFeed = new List<RssFeed>();
            tempFeed.Add(FeedReaderUtil.GetFeedFromUrl("https://arminreiter.com/feed").Result);
            tempFeed.Add(FeedReaderUtil.GetFeedFromUrl("https://habr.com/ru/rss/all/all/?fl=ru").Result);
            tempFeed.Add(FeedReaderUtil.GetFeedFromUrl("https://lenta.ru/rss/news").Result);
            tempFeed.Add(FeedReaderUtil.GetFeedFromUrl("https://www.vedomosti.ru/rss/news").Result);
            tempFeed.Add(FeedReaderUtil.GetFeedFromUrl("https://ria.ru/export/rss2/archive/index.xml").Result);
            tempFeed.Add(FeedReaderUtil.GetFeedFromUrl("https://www.mk.ru/rss/index.xml").Result);
            return tempFeed;
        }
        public IActionResult Index()
        {
            feeds = getFeeds();
            return View(feeds);
        }
        [HttpPost]
        public IActionResult Index(string tag)
        {
            if (tag == null) return Index();
            var taggedFeed = new List<RssFeed>();
            feeds = getFeeds();
            foreach (var feed in feeds)
            {
                var taggedArticle = feed.GetArticleByTag(tag);
                if (taggedArticle == null || taggedArticle.Count == 0) continue;
                taggedFeed.Add(new RssFeed(-1, feed.Title, feed.Description, feed.Image));

                //We fill current feed by articles by tag
                taggedFeed[taggedFeed.Count - 1].AddArticles(taggedArticle);
            }
            if (taggedFeed.Count <= 0) taggedFeed = null;
            return View(taggedFeed);
        }

    }
}
