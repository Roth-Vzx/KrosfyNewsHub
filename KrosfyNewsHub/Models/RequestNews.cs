using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KrosfyNewsHub.Models
{
    public class Article
    {
        public Source source { get; set; }
        public string author { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string url { get; set; }
        public string urlToImage { get; set; }
        public DateTime publishedAt { get; set; }
        public string content { get; set; }
    }

    public class Content
    {
        public string status { get; set; }
        public int totalResults { get; set; }
        public List<Article> articles { get; set; }
    }

    public class News
    {
        public int subcategory { get; set; }
        public int category { get; set; }
        public Content content { get; set; }
    }

    public class RequestNews
    {
        public List<News> News { get; set; }
    }

    public class Source
    {
        public string id { get; set; }
        public string name { get; set; }
    }

}