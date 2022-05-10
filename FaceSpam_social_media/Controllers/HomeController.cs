using FaceSpam_social_media.Models;
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
        public static Main userProfileModel = new Main();
        public DbModels.mydbContext context = new DbModels.mydbContext();
        public static MessagesForm messages = new MessagesForm();
        public static FriendsViewModel friendsModel = new FriendsViewModel();
        public static PostCommentsModel commentsModel = new PostCommentsModel();
        public static LoginModel loginModel = new LoginModel();
        public static SettingsModel settingsModel = new SettingsModel();
        public static AuthenticationModel authModel = new AuthenticationModel();
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public int RemoveMessage(int messageId)
        {
            int result = 0;
            result = messages.RemoveMessage(context, messageId);
            return result;
        }

        [HttpPost]
        public int RemovePost(int postId)
        {
            int result = 0;
            result = mainFormModels.RemovePost(context, postId);
            return result;
        }

        [HttpPost]
        public IActionResult Comments(int id)
        { 
            commentsModel.user = mainFormModels.user;
            commentsModel.post = context.Posts.Where(x => x.PostId == id).FirstOrDefault();
            commentsModel.GetComments(context);

            return View("Comments", commentsModel);
        }

        public IActionResult AddComment(string message)
        {
            commentsModel.AddComment(context, message);

            return View("Comments", commentsModel);
        }

        public DbModels.User GetUser() {

            return mainFormModels.user;
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
        public IActionResult UserProfile(int id) 
        {
            userProfileModel.GetUserInfo(context, id);
            userProfileModel.mainUserId = mainFormModels.user.UserId;
            userProfileModel.isFriend = mainFormModels.IsFriend(id);

            return View("Main", userProfileModel);
        }

        public IActionResult Friends(int id)
        {
            friendsModel.GetUserById(context, id);
            if (friendsModel.allUsers.Count > 0)
            {
                friendsModel.allUsers.Clear();
            }
            friendsModel.friendPage = true;
            friendsModel.mainUserId = mainFormModels.user.UserId;
            return View(friendsModel);
        }

        public IActionResult UserList()
        {
            friendsModel.GetAllUsers(context);
            friendsModel.friendPage = false;
            friendsModel.mainUserId = mainFormModels.user.UserId;

            return View("Friends", friendsModel);
        }

        public void DeleteFriend(int id)
        {
            userProfileModel.isFriend = mainFormModels.IsFriend(id);

            friendsModel.DeleteFriend(context, id);
            mainFormModels.GetFriends(context);
        }

        public void AddFriend(int id)
        {
            userProfileModel.isFriend = mainFormModels.IsFriend(id);

            friendsModel.AddFriend(context, id);
            mainFormModels.GetFriends(context);
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
        public (DbModels.User, int) AddPost(IFormFile file, string text)
        {
            string image_ref = null;
            if (file != null)
            {
                string extension = Path.GetExtension(file.FileName);
                if(extension != ".jpg" && extension != ".png")
                {
                    return (mainFormModels.user, -1);
                }
                image_ref = "../Images/" + file.FileName;
                AddImageToPost(file);
            }
            int lastPostId = mainFormModels.AddPost(context, text, image_ref);
            return (mainFormModels.user, lastPostId);
        }

        public async void AddImageToPost(IFormFile file)
        {
            string path = "./wwwroot/Images/" + file.FileName;
            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
                fileStream.Close();
            }
        }

        public (DbModels.User, int) SendMessage(string textboxMessage)
        {
            int id = messages.SendMessage(context, textboxMessage);
            return (messages.user, id);
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
            settingsModel.user = mainFormModels.user;
            return View(settingsModel);
        }
        public IActionResult ChangeUserInfo(string email, string name, string description)
        {
            settingsModel.ChangeUserInfo(context, email, name, description);
            mainFormModels.user.Name = settingsModel.user.Name;
            mainFormModels.user.Email = settingsModel.user.Email;
            mainFormModels.user.Description = settingsModel.user.Description;

            commentsModel.user = mainFormModels.user;
            friendsModel.user = mainFormModels.user;

            return View("Main", mainFormModels);
        }
        
        public IActionResult Authentication()
        {
            return View();
        }

        [HttpPost]
        public IActionResult VerifyUserAuthentication(string login, string password, string email)
        {
            authModel.Login = login;
            authModel.Password = password;
            authModel.Email = email;
            bool repeatCheck = authModel.Verify(context);

            if (repeatCheck)
            {
                return Content("This user is already registered.");
            }
            else {
                authModel.CreateUser(login, password, email, context);
                mainFormModels.GetMainUserInfo(context, login, password);
                return View("Main", mainFormModels);
            }
        }
    }
}
