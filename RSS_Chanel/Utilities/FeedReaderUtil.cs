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
    }
}
