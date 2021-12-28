using ExamApp.Models;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ExamApp.Repositories
{
    public class RssFeedRepository : IRssFeedRepository
    {
        private string rssUrl = "https://www.wired.com/rss/";

        public async Task<IEnumerable<RssFeedModel>> GetFeeds()
        {
            Uri adress = new Uri(rssUrl);
            WebClient wclient = new WebClient();
            var RSSData =await wclient.DownloadStringTaskAsync(adress);

            XDocument xml = XDocument.Parse(RSSData);
            var RSSFeedData = (from x in xml.Descendants("item")
                               select new RssFeedModel
                               {
                                   id = Guid.NewGuid().ToString(),
                                   Title = ((string)x.Element("title")),
                                   Link = ((string)x.Element("link")),
                                   Description = ((string)x.Element("description"))
                                   
                               });
            return RSSFeedData.Take(5);
        }
        public async Task<string> GetFeedContent(string url)
        {
            HttpClient hc = new HttpClient();
            HttpResponseMessage result = await hc.GetAsync(url);

            Stream stream = await result.Content.ReadAsStreamAsync();

            HtmlDocument doc = new HtmlDocument();

            doc.Load(stream);

            var root = doc.DocumentNode;
            var commonPosts = root.Descendants("article").FirstOrDefault().Descendants("p");
            var htmlStr = "";
            foreach (var commonPost in commonPosts)
            {
                htmlStr = htmlStr + commonPost.InnerHtml.ToString();
            }
            return htmlStr;
           
        }
    }
}
