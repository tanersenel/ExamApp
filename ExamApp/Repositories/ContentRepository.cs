using ExamApp.Data;
using ExamApp.Entities;
using System.Threading.Tasks;

namespace ExamApp.Repositories.Interfaces
{
    public class ContentRepository : IContentRepository
    {
        private readonly ExamContext _examContext;
        public Content CreateContent(Content content)
        {
            throw new System.NotImplementedException();
        }

        public bool DeleteAllContent()
        {
            throw new System.NotImplementedException();
        }

        public bool DeleteContent(string id)
        {
            throw new System.NotImplementedException();
        }
    }
}
