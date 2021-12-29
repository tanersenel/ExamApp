using ExamApp.Data;
using ExamApp.Entities;
using ExamApp.Models;
using ExamApp.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
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

            var content = _examContext.Content.FirstOrDefault(x => x.id == questions.First().ExamId);
            await _examContext.Exam.AddAsync(new Exam() { ContentId = content.id, id = content.id, CreatedDate = DateTime.Now, Questions = questions, Title = content.Title,Content = content  });
            
            _examContext.SaveChanges();
            return true;
        }

        public  bool DeleteExam(string id)
        {
            var questions = _examContext.Question.Where(x => x.ExamId == id).ToList();
            _examContext.Question.RemoveRange(questions);
            _examContext.SaveChanges();
            var exam =  _examContext.Exam.FirstOrDefault(x => x.id == id);
            var result = _examContext.Exam.Remove(exam);
            _examContext.SaveChanges();
            return true;
        }

        public  IEnumerable<Exam> GetAllExams() => _examContext.Exam.ToList();

        public Exam GetExam(string id) => _examContext.Exam.Include(x=>x.Content).Include(x=>x.Questions).FirstOrDefault(x => x.id == id);
        
    }
}
