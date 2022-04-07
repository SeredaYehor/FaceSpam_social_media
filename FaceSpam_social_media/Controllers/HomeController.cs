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
        private static Post postModel = new Post("W1ld3lf", "Check out this view!!!", new DateTime(20, 3, 11, 13, 0, 0), "W1ld3lf.png", "post.jpg");

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            postModel.postComments.Add(new PostComment("Elon Mask", "Great post-nuclear avantgarde view)", DateTime.Now, "Mask.jpg"));
            postModel.postComments.Add(new PostComment("Митрополит Вадим", "Господь, господь", DateTime.Now, "Vadim.png"));

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
                postModel.postComments.Add(new PostComment("W1ld3lf", message, DateTime.Now, "W1ld3lf.png"));
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
