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
        public static LoginModel loginModel = new LoginModel();
        public static SettingsModel settingsModel = new SettingsModel();
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

        [HttpPost]
        public IActionResult Main()
        {
            return View(mainFormModels);
        }

        [HttpPost]
        public IActionResult VerifyUserLogin(string login, string password)
        {
            loginModel.Login = login;
            loginModel.Password = password;

            bool verifyResult = loginModel.Verify(context);
            if (verifyResult)
            {
                mainFormModels.GetMainUserInfo(context, login, password);
                return View("Main", mainFormModels);
            }
            else { return Content("Wrong login or password"); }
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
        
        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Settings()
        {
            return View();
        }
        public IActionResult ChangeUserInfo(string email, string name, string description)
        {
            settingsModel.user = mainFormModels.user;
            settingsModel.ChangeUserInfo(context, email, name, description);
            mainFormModels.user = settingsModel.user;
            return View("Main", mainFormModels);
        }
    }
}
