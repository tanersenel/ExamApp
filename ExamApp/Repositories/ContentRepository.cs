using ExamApp.Data;
using ExamApp.Entities;
using ExamApp.Extensions;
using System.Linq;
using System.Threading.Tasks;

namespace ExamApp.Repositories.Interfaces
{
    public class ContentRepository : IContentRepository
    {
        private readonly ExamContext _examContext;
        public ContentRepository(ExamContext examContext)
        {
            _examContext = examContext; 

        }
        public Content CreateContent(Content content)
        {
            _examContext.Content.Add(content);
            return content; 
        }

        public bool DeleteAllContent()
        {
            _examContext.Content.Clear();
            return true;
        }

        public bool DeleteContent(string id)
        {
            var content = _examContext.Content.FirstOrDefault(x => x.id == id);
            if (content != null)  _examContext.Content.Remove(content);
            return true;
            
            
        }
    }
}
