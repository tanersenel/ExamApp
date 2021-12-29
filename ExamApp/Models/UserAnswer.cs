using System.Collections.Generic;

namespace ExamApp.Models
{
    public class UserAnswer
    {
        public string ExamId { get; set; }
        public List<UserAnswerQuestion> Answers { get; set; }

    }
    public class UserAnswerQuestion
    {
        public string id { get; set; }
        public string Answer { get; set; } 
        public string TrueAnswer { get; set; }
        public bool True { get; set; }
    }
}
