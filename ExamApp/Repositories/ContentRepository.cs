using ExamApp.Entities;
using System.Threading.Tasks;

namespace ExamApp.Repositories.Interfaces
{
    public class ContentRepository : IContentRepository
    {
        public Task<Content> CreateContent(Content content)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> DeleteAllContent()
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> DeleteContent(string id)
        {
            throw new System.NotImplementedException();
        }
    }
}
