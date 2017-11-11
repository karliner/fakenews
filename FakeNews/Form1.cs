using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FakeNews
{
    public partial class Form1 : Form
    {
        /// <summary>
        /// filters from the api the latest news that contain the keyword
        /// </summary>
        /// <returns>the matching articles</returns>
        public List<Article> getNews()
        {
            string search = searchText.Text;

            string html = string.Empty;
            string url = @"http://beta.newsapi.org/v2/top-headlines?q=" + search + "&language=en&apiKey=d9c0008e8b464c10a03ad283fc34921c";

            List<Article> filteredArticle = new List<Article>();

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.AutomaticDecompression = DecompressionMethods.GZip;

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                html = reader.ReadLine();
                JObject json = JObject.Parse(html);
                filteredArticle = JsonConvert.DeserializeObject<List<Article>>(json.Last.ToString().Substring(11));
            }
            filteredArticle = filteredArticle.OrderBy(o => o.PublishedAt).ToList();
            return filteredArticle;
        }

        /// <summary>
        /// checks which words show with the searched word and how many times and sources
        /// </summary>
        /// <param name="list">list of articles</param>
        /// <returns>words appearence and amout</returns>
        public List<Frequency> checkArticles(List<Article> list)
        {
            List<Frequency> words = new List<Frequency>();
            Frequency word;
            List<string> data = new List<string>();

            foreach (var item in list)
            {
                data = item
                    .Title
                    .Split(' ')
                    .ToList()
                    .Where(w=>w.Length > 0 && w[0] >= 'A' && w[0] <= 'Z' && !w.ToLower().Contains(searchText.Text.ToLower()))
                    .ToList();

                foreach (var dataItem in data)
                {
                    if(words.Where(w=>w.Word.Equals(dataItem)).Count() == 0)
                    {
                        word = new Frequency(dataItem);
                        words.Add(word);
                    }
                    words.SingleOrDefault(s => s.Word.Equals(dataItem)).Add(item.Source.Name, list.FindIndex(f=>f == item));
                }

            }

            foreach (var item in words)
            {
                item.Percentage = Math.Round(item.Amount / (double)list.Count * 100);
            }

            words = words.OrderByDescending(o => o.Percentage).Where(w=>w.Percentage >=5).ToList();

            return words;
        }

        /// <summary>
        /// windows form function
        /// </summary>
        public Form1()
        {
            InitializeComponent();
        }
        
        /// <summary>
        /// process the button click
        /// </summary>
        /// <param name="sender">default parameter for the clicked object</param>
        /// <param name="e">default parameter for the clicked object</param>
        private void Search_Click(object sender, EventArgs e)
        {
            List<Article> listArticle = getNews();
            List<Frequency> words = checkArticles(listArticle);
            articleData.DataSource = words;
        }
    }
}
