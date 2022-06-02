using CodeHollow.FeedReader;
using RSS_Chanel.Models;

namespace RSS_Chanel.Utilities
{
    public static class FeedReaderUtil
    {
        public async static Task<RssFeed> GetFeedFromUrl(string url)
        {
            var feed = await FeedReader.ReadAsync(url);

            if (feed.Items.Count == 0) return RssFeed.Empty;

            RssFeed rssFeed = new RssFeed(0, feed.Title, feed.Description, feed.ImageUrl);

            foreach (var item in feed.Items)
            {
                var article = new Article(item.Title,item.Link);
                rssFeed.AddArticles(article);
            }

            return rssFeed;
        }
        public static RssFeed GetTopics(string url)
        {
            var temp = GetTopicsAsync(url).Result;
            return temp;
        }
        private async static Task<RssFeed> GetTopicsAsync(string url)
        {
            List<RssFeed> feeds = new List<RssFeed>();
            List<string> Urls = new List<string>();

            var feed = await FeedReader.ReadAsync(url);
            if (feed.Items.Count == 0) return null;

            RssFeed rssFeed = new RssFeed(0, feed.Title, feed.Description, feed.ImageUrl);

            int maxArt = 5;
            int count = 0;
            foreach (var item in feed.Items)
            {
                count++;
                var article = new Article(item.Title, item.Link);
                rssFeed.AddArticles(article);
                if (count > maxArt)
                    return rssFeed;
            }
            return rssFeed;
        }
    }
}
