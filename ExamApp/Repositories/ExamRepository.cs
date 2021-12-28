using ExamApp.Data;
using ExamApp.Entities;
using ExamApp.Models;
using ExamApp.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExamApp.Repositories
{
    public class ExamRepository : IExamRepository
    {
        private readonly ExamContext _examContext;
        public ExamRepository(ExamContext examContext)
        {
            _examContext = examContext; 

        }
        public async Task<bool> AddQuestions(IEnumerable<Question> questions)
        {
            await _examContext.Question.AddRangeAsync(questions);
            _examContext.SaveChanges();
            return true;
        }

        public  bool DeleteExam(string id)
        {
            var exam =  _examContext.Exam.FirstOrDefault(x => x.id == id);
            var result = _examContext.Exam.Remove(exam);
            _examContext.SaveChanges();
            return true;
        }

        public  IEnumerable<Exam> GetAllExams() => _examContext.Exam.ToList();

        public Exam GetExam(string id) => _examContext.Exam.FirstOrDefault(x => x.id == id);
        
    }
}
