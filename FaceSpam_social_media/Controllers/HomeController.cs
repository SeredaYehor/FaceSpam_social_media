using FaceSpam_social_media.Models;
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
        public DbModels.mydbContext context = new DbModels.mydbContext();
        public static MessagesForm messages = new MessagesForm();

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
        public IActionResult AddPost(Main model)
        {
            mainFormModels.AddPost(context, model.message);
            return View("Main", mainFormModels);
        }

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
        
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
