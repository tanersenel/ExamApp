using ExamApp.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExamApp.Repositories.Interfaces
{
    public interface IContentRepository
    {
        Content CreateContent(Content content);
        bool DeleteContent(string id);
        bool DeleteAllContent();
        IEnumerable<Content> GetAlContent();



    }
}
