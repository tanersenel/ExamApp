using ExamApp.Entities;
using ExamApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExamApp.Repositories.Interfaces
{
    public interface IExamRepository
    {
        IEnumerable<Exam> GetAllExams();
        Exam GetExam(string id);
        bool DeleteExam(string id);
        Task<bool> AddQuestions(IEnumerable<Question> questions);

    }
}
