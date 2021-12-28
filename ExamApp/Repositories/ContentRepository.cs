using ExamApp.Data;
using ExamApp.Entities;
using ExamApp.Extensions;
using System.Collections.Generic;
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
            _examContext.SaveChanges();
            return content; 
        }

        public bool DeleteAllContent()
        {
            _examContext.Content.Clear();
            _examContext.SaveChanges();
            return true;
        }

        public bool DeleteContent(string id)
        {
            var content = _examContext.Content.FirstOrDefault(x => x.id == id);
            if (content != null)  _examContext.Content.Remove(content);
            _examContext.SaveChanges();
            return true;
            
            
        }

        public IEnumerable<Content> GetAlContent()
        {
            return _examContext.Content.ToList();
        }
    }
}
