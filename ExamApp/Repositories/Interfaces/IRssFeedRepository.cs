using ExamApp.Models;
using HtmlAgilityPack;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExamApp.Repositories
{
    public interface IRssFeedRepository
    {
        Task<IEnumerable<RssFeedModel>> GetFeeds();
        Task<string> GetFeedContent(string url);

    }
}
