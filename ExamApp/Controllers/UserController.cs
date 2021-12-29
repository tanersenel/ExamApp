using ExamApp.Models;
using ExamApp.Repositories;
using ExamApp.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace ExamApp.Controllers
{
    public class UserController : Controller
    {
        private readonly ILogger<UserController> _logger;
        private readonly IRssFeedRepository _rssRepository;
        private readonly IUserRepository _userRepository;
        private readonly IContentRepository _contentRepository;
        private readonly IExamRepository _examRepository;
        public UserController(ILogger<UserController> logger, IRssFeedRepository rssRepository, IUserRepository userRepository, IContentRepository contentRepository, IExamRepository examRepository)
        {
            _rssRepository = rssRepository;
            _userRepository = userRepository;
            _contentRepository = contentRepository;
            _examRepository = examRepository;
            _logger = logger;
        }
        public IActionResult Index()
        {
            var user = HttpContext.Session.GetString("user");
            if (user is null) return RedirectToAction("Login", "Home");

            var exams = _examRepository.GetAllExams();

            return View(exams);
           
        }
        public IActionResult Exam(string id)
        {
            var user = HttpContext.Session.GetString("user");
            if (user is null) return RedirectToAction("Login", "Home");

            var exam = _examRepository.GetExam(id);

            return View(exam);

        }
        [HttpPost]
        public JsonResult FinishExam(UserAnswer answers)
        {
            var user = HttpContext.Session.GetString("user");
            if (user is null) return null;
            var exam = _examRepository.GetExam(answers.ExamId);
            foreach (var ans in answers.Answers)
            {
                var q = exam.Questions.FirstOrDefault(x => x.id == ans.id);
                ans.True = q.Answer == ans.Answer;
                ans.TrueAnswer = q.Answer;

            }
           
            return Json(answers);
        }
    }
}
