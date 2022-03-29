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
        public static UserChats group = new UserChats();
        public HomeController(ILogger<HomeController> logger)
        {
            
            _logger = logger;
        }

        [HttpPost]
        public void SetId(int id)
        {
            group.chatId = id;
        }
        public int GetId()
        {
            return group.chatId;
        }
        public IActionResult Index()
        {
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
            mainFormModels.user = new User(1, "W1ld 3lf", "12345", "ogo@mail.com",
            "Кодер на миллион", null);
            mainFormModels.posts.Add(new Post(1, "Hi everyone!"));
            mainFormModels.posts.Add(new Post(2, "Wats up?"));
            mainFormModels.posts.Add(new Post(3, "This is an example post and bla bla bla" +
                "bla bla bla bla bla bla bla bla bla bla bla bla bla bla bla"));
            return View(mainFormModels);
        }

        public IActionResult Messages()
        {
            group.chats.Add(new Chat(1, "Dorenskiy O. P.", "Some chat", 2, DateTime.Now, "../images/Cyberdemon.png"));
            //Bug!!!  when adding messages chats also appends>(
            group.chats[0].chatMessages.Add(new Message(4, "Prikol",
                DateTime.Now, 1, 0, 1));
            group.chats.Add(new Chat(2, "FaceSpam Community", "Some chat", 2, DateTime.Now, "../images/facespam.png"));
            group.chats[1].chatMessages.Add(new Message(4, "Woobshe",
                DateTime.Now, 2, 0, 1));
            group.chats.Add(new Chat(3, "Elon Musk", "Some chat", 2, DateTime.Now, "../images/Mask.jpg"));
            group.chats[2].chatMessages.Add(new Message(4, "Ulyot",
               DateTime.Now, 3, 0, 1));
            return View(group);
        }

        public void SendMessage(string textboxMessage)
        {
            group.chats[group.GetChatId(group.chatId)].chatMessages.Add(new Message(4, textboxMessage, 
                DateTime.Now, 1, 0, group.GetChatId(group.chatId)));
        }

        public List<Message> GetChatMessages()
        {
            List<Message> result = group.chats[group.GetChatId(group.chatId)].chatMessages;
            return result;
        }

        [HttpPost]
        public IActionResult AddPost(Main model)
        {
            mainFormModels.posts.Add(new Post(4, model.message));
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
