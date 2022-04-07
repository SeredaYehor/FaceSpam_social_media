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
        private static Post postModel = new Post("W1ld3lf", "Check out this view!!!", GetCurrentTime(), "W1ld3lf.png", "post.jpg");

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            postModel.postComments.Add(new PostComment("Elon Mask", "Great post-nuclear avantgarde view)", GetCurrentTime(), "Mask.jpg"));
            postModel.postComments.Add(new PostComment("Митрополит Вадим", "Господь, господь", GetCurrentTime(), "Vadim.png"));

            return View();
        }

        public IActionResult Comments(string message = null)   
        {
            return View(postModel);
        }

        public IActionResult AddComment(string message)
        {
            if (message != null)
            {
                postModel.postComments.Add(new PostComment("W1ld3lf", message, GetCurrentTime(), "W1ld3lf.png"));
            }

            return View("Comments", postModel);
        }

        static public string GetCurrentTime()
        {
            string date = DateTime.Now.Date.ToString().Remove(5, 13) + "." + DateTime.Now.Year.ToString().Remove(0, 2);
            string time = DateTime.Now.ToShortTimeString();

            return date + " " + time;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
