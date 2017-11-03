using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FakeNews
{
    /// <summary>
    /// Class for the api data
    /// </summary>
    public class Article
    {
        private Source source;
        private string author;
        private string title;
        private string description;
        private string url;
        private string urlToImage;
        private string publishedAt;

        public Source Source { get => source; set => source = value; }
        public string Author { get => author; set => author = value; }
        public string Title { get => title; set => title = value; }
        public string Description { get => description; set => description = value; }
        public string Url { get => url; set => url = value; }
        public string UrlToImage { get => urlToImage; set => urlToImage = value; }
        public string PublishedAt { get => publishedAt; set => publishedAt = value; }
    }
}
