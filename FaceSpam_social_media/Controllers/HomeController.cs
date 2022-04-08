﻿using FaceSpam_social_media.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace FaceSpam_social_media.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public static Main mainFormModels = new Main();
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            mainFormModels.user = new User(777, "W1ld 3lf", "12345", "ogo@mail.com",
            "Кодер на миллион", null);
            mainFormModels.posts.Add(new Post(1, "Hi everyone!"));
            mainFormModels.posts.Add(new Post(2, "Wats up?"));
            mainFormModels.posts.Add(new Post(3, "This is an example post and bla bla bla" +
                "bla bla bla bla bla bla bla bla bla bla bla bla bla bla bla"));
            for (int i = 1; i < 4; i++)
            {
                int postId = mainFormModels.GetPostIndex(i);
                for (int j = 0; j < 5; j++)
                {
                    mainFormModels.posts[postId].UpdateLike(j);
                }
            }
            mainFormModels.posts[0].UpdateLike(777);
            return View();
        }

        public IActionResult ControllerTest()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult ShowData(string data)
        {
            ViewData["string"] = data; 

            return View();
        }

        public IActionResult Main()
        {
            return View(mainFormModels);
        }

        public int ChangeLike(int postId)
        {
            mainFormModels.posts[postId].UpdateLike(777);
            return mainFormModels.posts[postId].CountLikes();
        }

        [HttpPost]
        public IActionResult AddPost(Main model)
        {
            mainFormModels.posts.Insert(0, new Post(4, model.message));
            ModelState.Clear();
            return View("Main", mainFormModels);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
