namespace RSS_Chanel.Models
{
    public class RssFeed
    {
        public RssFeed(int id, string title, string description, string image,List<Article> articles = null)
        {
            //скорее всего id не нужно 
            //*если попросит сделать бд*
            Id = id;
            Title = title;
            Description = description;
            Image = image;
        }
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }

        public List<Article> Articles = new List<Article>();

        public static RssFeed Empty
        {
            get
            {
                return new RssFeed();
            }
        }
        private RssFeed() { }
        public void AddArticles(Article article)
        {
            Articles.Add(article);
        }
        public void AddArticles(List<Article> articles)
        {
            Articles.AddRange(articles);
        }
        public List<Article> GetArticleByTag(string tag)
        {
            if(Articles.Count == 0 || tag == null) return null;

            List<Article> taggedArticles = new List<Article>();
            foreach (var article in Articles)
            {
                if (article.Title.Contains(tag))
                {
                    taggedArticles.Add(article);
                }
            }
            return taggedArticles;
        }
    }

    public class Article
    {
        public Article(string title, string link)
        {
            Title = title;
            Link = link;
        }
        public string Title { get; set; }
        public string Link { get; set; }
    }
}
