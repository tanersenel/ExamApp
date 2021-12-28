using ExamApp.Entities;
using ExamApp.Models;
using ExamApp.Repositories;
using ExamApp.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ExamApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IRssFeedRepository _rssRepository;
        private readonly IUserRepository _userRepository;
        private readonly IContentRepository _contentRepository;


        public HomeController(ILogger<HomeController> logger, IRssFeedRepository rssRepository, IUserRepository userRepository, IContentRepository contentRepository)
        {
            _rssRepository = rssRepository;
            _userRepository = userRepository;
            _contentRepository = contentRepository; 
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
           var feeds= await _rssRepository.GetFeeds();
            foreach (var item in feeds)
            {
                var html = await _rssRepository.GetFeedContent(item.Link);
                var content = new Content()
                {
                    id = Guid.NewGuid().ToString(),
                    CreatedDate = DateTime.Now,
                    Descriiption = item.Description,
                    Link = item.Link,
                    Title = item.Title,
                    ContentText = html.ToString()
                };
                _contentRepository.CreateContent(content);
            }
            
            
            return View(_contentRepository.GetAlContent());
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
