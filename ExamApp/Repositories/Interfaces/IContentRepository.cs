using ExamApp.Entities;
using System.Threading.Tasks;

namespace ExamApp.Repositories.Interfaces
{
    public interface IContentRepository
    {
        Task<Content> CreateContent(Content content);
        Task<bool> DeleteContent(string id);
        Task<bool> DeleteAllContent();
        


    }
}
