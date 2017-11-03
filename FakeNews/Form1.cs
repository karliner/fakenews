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
            return filteredArticle;
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
            articleData.DataSource = listArticle;
            articleData.Columns[3].Visible = false;
            articleData.Columns[5].Visible = false;
        }
    }
}
