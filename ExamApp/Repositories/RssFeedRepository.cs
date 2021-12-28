using ExamApp.Extensions;
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

        private string rssUrl = "https://www.wired.com/sitemap/?month="+DateTime.Now.Month+"&week="+DateTime.Now.GetWeekNumberOfMonth().ToString()+"&year="+DateTime.Now.Year;

        public async Task<IEnumerable<FeedModel>> GetFeeds()
        {
            HttpClient hc = new HttpClient();
            HttpResponseMessage result = await hc.GetAsync(rssUrl);

            Stream stream = await result.Content.ReadAsStreamAsync();

            HtmlDocument doc = new HtmlDocument();

            doc.Load(stream);

            var root = doc.DocumentNode;
            var commonPosts = root.SelectSingleNode("//div[@class='sitemap__section-archive']").Descendants("li").ToArray();
            Array.Reverse(commonPosts);
            var newcommonPosts = commonPosts.ToList().Take(5);
           
            var RSSFeedData = (from x in newcommonPosts
                               select new FeedModel
                               {
                                   id = Guid.NewGuid().ToString(),
                                   CreatedDate = DateTime.Now,
                                   Link = x.InnerText
                                   
                               });
            return RSSFeedData.Take(5);

          
        }
        public async Task<FeedDetailModel> GetFeedContent(string url)
        {
            HttpClient hc = new HttpClient();
            HttpResponseMessage result = await hc.GetAsync(url);

            Stream stream = await result.Content.ReadAsStreamAsync();

            HtmlDocument doc = new HtmlDocument();

            doc.Load(stream);

            var root = doc.DocumentNode;
            var commonPosts = root.Descendants("article").FirstOrDefault().Descendants("p");
            var title = root.Descendants("title").FirstOrDefault();
            var htmlStr = "";
            foreach (var commonPost in commonPosts)
            {
                htmlStr = htmlStr + commonPost.InnerHtml.ToString();
            }
            return new FeedDetailModel()
            {
                Title = title.InnerText.Replace("| WIRED",""),
                HtmlStr = htmlStr
            };
           
        }
    }
}

