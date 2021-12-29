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
using Microsoft.AspNetCore.Http;
using System.Text.Json;

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
          
            
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login([FromForm] User user)
        {
           var response =  _userRepository.Login(user);
            if (response.Status) {
                string jsonString = JsonSerializer.Serialize(response.Response);
                
                var loginuser = (User)response.Response;
                if (loginuser.UserType == (int)UserType.Admin)
                {
                    HttpContext.Session.SetString("admin", jsonString);
                    return RedirectToAction("Index", "Panel");
                }                     
                else if (loginuser.UserType == (int)UserType.User)
                {
                    HttpContext.Session.SetString("user", jsonString);
                    return RedirectToAction("Index", "User");
                }
                    
            }
            ViewBag.Error = response.Error;
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
