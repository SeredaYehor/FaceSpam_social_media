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
        protected static FriendsModel friends = new FriendsModel();

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

        public IActionResult Comments() {
            return View();
        }
        public int GetId()
        {
            return group.chatId;
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
            friends.userName.Add(new UserModel("Dorenskyi Aleksandr", "Cyberdemon.png"));
            friends.userName.Add(new UserModel("Mitropolit Vadim", "Vadim.png"));
            friends.userName.Add(new UserModel("Elon Mask", "Mask.jpg"));
            friends.userName.Add(new UserModel("Kenoby", "Kenoby.jpg"));

            group.chats.Add(new Chat(1, "Dorenskiy O. P.", "Some chat", 2, DateTime.Now, "../images/Cyberdemon.png"));
            group.chats[0].chatMessages.Add(new Message(4, "Prikol",
                DateTime.Now, 1, 0, 1));
            group.chats.Add(new Chat(2, "FaceSpam Community", "Some chat", 2, DateTime.Now, "../images/facespam.png"));
            group.chats[1].chatMessages.Add(new Message(4, "Woobshe",
                DateTime.Now, 2, 0, 1));
            group.chats.Add(new Chat(3, "Elon Musk", "Some chat", 2, DateTime.Now, "../images/Mask.jpg"));
            group.chats[2].chatMessages.Add(new Message(4, "Ulyot",
               DateTime.Now, 3, 0, 1));
            return View();
        }

        public IActionResult Friends()
        {
            return View(friends);
        }

        public IActionResult Settings()
        {
            return View();
        }

        public void DeleteFriend(string name)
        {
            UserModel removeUser = friends.GetUser(name);
            friends.userName.Remove(removeUser);
        }

        public IActionResult Messages()
        {
            return View(group);
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

        public IActionResult Login()
        {
            return View();
        }
        
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        
        public IActionResult Authentication()
        {
            return View();
        }
    }
}
