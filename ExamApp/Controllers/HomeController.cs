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


        public HomeController(ILogger<HomeController> logger, IRssFeedRepository rssRepository, IUserRepository userRepository)
        {
            _rssRepository = rssRepository;
            _userRepository = userRepository;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
           var feeds= await _rssRepository.GetFeeds();
            var html =await _rssRepository.GetFeedContent(feeds.FirstOrDefault().Link);
            
            return View(html);
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
