using ExamApp.Entities;
using ExamApp.Repositories;
using ExamApp.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExamApp.Controllers
{
    public class PanelController : Controller
    {
        private readonly ILogger<PanelController> _logger;
        private readonly IRssFeedRepository _rssRepository;
        private readonly IUserRepository _userRepository;
        private readonly IContentRepository _contentRepository;
        private readonly IExamRepository _examRepository;

        public PanelController(ILogger<PanelController> logger, IRssFeedRepository rssRepository, IUserRepository userRepository, IContentRepository contentRepository,IExamRepository examRepository)
        {
            _rssRepository = rssRepository;
            _userRepository = userRepository;
            _contentRepository = contentRepository;
            _examRepository = examRepository;    
            _logger = logger;
        }

        public IContentRepository ContentRepository => _contentRepository;

        public IActionResult Index()
        {
           var user= HttpContext.Session.GetString("user");
            if(user is null) return RedirectToAction("Login", "Home");

            var exams = _examRepository.GetAllExams();
            
            return View(exams);
        }
        public async Task<IActionResult> Add()
        {
            var user = HttpContext.Session.GetString("user");
            if (user is null) return RedirectToAction("Login", "Home");

            var feeds = await _rssRepository.GetFeeds();
            foreach (var item in feeds)
            {
                var html = await _rssRepository.GetFeedContent(item.Link);
                var content = new Content()
                {
                    id = Guid.NewGuid().ToString(),
                    CreatedDate = DateTime.Now,
                    Descriiption = item.Description,
                    Link = item.Link,
                    Title = html.Title,
                    ContentText = html.HtmlStr.ToString()
                };
                var cnt = _contentRepository.GetContentWithLink(item.Link);
                if(cnt is null)
                _contentRepository.CreateContent(content);
            }


            return View(_contentRepository.GetAllContent());
        }
        public JsonResult GetContentDetail(string contentid)
        {
            var user = HttpContext.Session.GetString("user");
            if (user is null) return null;
            var content = _contentRepository.GetContent(contentid);
           
            return Json(content);
        }
        [HttpPost]
        public JsonResult CreateExam(List<Question> questions)
        {
            var user = HttpContext.Session.GetString("user");
            if (user is null) return null;
            var content = _examRepository.AddQuestions(questions);
            if(content.IsFaulted)
            {
                return Json(content.Exception.InnerException.InnerException.Message);
            }
            return Json(content.Result);
        }
        [HttpPost]
        public JsonResult DeleteExam(string examid)
        {
            var user = HttpContext.Session.GetString("user");
            if (user is null) return null;
            var content = _examRepository.DeleteExam(examid);
            if (!content)
            {
                return Json("Error");
            }
            return Json("OK");
        }

    }
}
