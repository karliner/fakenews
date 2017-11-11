using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FakeNews
{
    /// <summary>
    /// class for holding each word that appeared with the searched word and how many times and where
    /// </summary>
    public class Frequency
    {
        private string word;
        private int amount;
        private List<string> sources;
        private List<int> articlePlaces;
        private double percentage;
        private string outputSources;
        /// <summary>
        /// creates an object of the word
        /// </summary>
        /// <param name="word">the word that appeared</param>
        public Frequency(string word)
        {
            this.Word = word;
            this.Amount = 0;
            this.Sources = new List<string>();
            this.ArticlePlaces = new List<int>();
            this.Percentage = 0;
            this.outputSources = "";
        }

        /// <summary>
        /// counts an apperance of the word
        /// </summary>
        /// <param name="source">the source the word came from</param>
        /// <param name="place">the place in the article list</param>
        public void Add(string source,int place)
        {
            this.amount++;
            this.Sources.Add(source);
            this.ArticlePlaces.Add(place);
            this.outputSources = this.outputSources + "," + source;
        }

        public string Word { get => word; set => word = value; }
        public int Amount { get => amount; set => amount = value; }
        public List<string> Sources { get => sources; set => sources = value; }
        public List<int> ArticlePlaces { get => articlePlaces; set => articlePlaces = value; }
        public double Percentage { get => percentage; set => percentage = value; }
        public string OutputSources { get => outputSources; set => outputSources = value; }
    }
}
