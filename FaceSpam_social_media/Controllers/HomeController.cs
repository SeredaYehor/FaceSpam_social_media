﻿using FaceSpam_social_media.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;

namespace FaceSpam_social_media.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public static Main mainFormModels = new Main();
        public DbModels.mydbContext context = new DbModels.mydbContext();
        public static MessagesForm messages = new MessagesForm();
        public static FriendsViewModel friendsModel = new FriendsViewModel();

        public HomeController(ILogger<HomeController> logger)
        {
            
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }


        public IActionResult Messages()
        {
            messages.user = mainFormModels.user;
            messages.GetChats(context);
            return View(messages);
        }

        public IActionResult Main()
        {
            mainFormModels.GetUser(context, "Wild 3lf", "ne_pass");
            mainFormModels.GetPosts(context);
            mainFormModels.GetFriends(context);
            mainFormModels.GetLikes(context);
            return View(mainFormModels);
        }

        public int ChangeLike(int postId)
        {
            mainFormModels.UpdatePostLike(context, postId);
            return mainFormModels.CountLikes(postId);
        }

        [HttpPost]
        public async void AddPost(IFormFile file, string text)
        {
            string image_ref = null;
            if (file != null)
            {
                string path = "./wwwroot/Images/" + file.FileName;
                image_ref = "../Images/" + file.FileName;
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                   await file.CopyToAsync(fileStream);
                   fileStream.Close();
                }
            }
            mainFormModels.AddPost(context, text, image_ref);
        }
        /*[HttpPost]
        public IActionResult AddPost(Main model)
        {
            mainFormModels.AddPost(context, model.message);
            return View("Main", mainFormModels);
        }*/

        public DbModels.User SendMessage(string textboxMessage)
        {
            messages.SendMessage(context, textboxMessage);
            return messages.user;
        }

        public List<DbModels.Message> GetChatMessages(int chatId)
        {
            messages.GetChatMessages(context, chatId);
            return messages.chatMessages;
        }

        public IActionResult Friends()
        {
            friendsModel.user = mainFormModels.user;
            friendsModel.friends = mainFormModels.friends;

            return View(friendsModel);
        }

        public void DeleteFriend(int id)
        {
            friendsModel.DeleteFriend(context, id);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
