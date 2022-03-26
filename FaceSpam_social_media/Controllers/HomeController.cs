using FaceSpam_social_media.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace FaceSpam_social_media.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private static Post postModel = new Post("Vadim", "Best palce to cheel is 715", new DateTime(2020, 3, 11, 13, 0, 0));

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Comments(string message = null)   
        {
            postModel.postComments.Add(new PostComment("CyberDemon", "2 years passed, where is lab 12", DateTime.Now));
            postModel.postComments.Add(new PostComment("CyberDemon", "Also u forgot about Prat book buing", DateTime.Now));

            return View(postModel);
        }

        [HttpPost]
        public IActionResult AddComment(string message)
        {
            string name = "Vadim";
            if (message != null)
            {
                postModel.postComments.Add(new PostComment(name, message, DateTime.Now));
            }

            return View("Comments", postModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
