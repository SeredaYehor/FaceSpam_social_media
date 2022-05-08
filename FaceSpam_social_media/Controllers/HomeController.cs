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
        public static UsersManagment usersManagment = new UsersManagment();
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Admin()
        {
            if(usersManagment.Init(context, mainFormModels.user))
            {
                return View(usersManagment);
            }
            return Content("Cannot get this page.");
        }

        [HttpPost]
        public int UpdateUserStatus(int userId)
        {
            int result = usersManagment.UpdateStatus(context, userId);
            return result;
        }

        [HttpPost]
        public int RemoveMessage(int messageId)
        {
            int result = 0;
            result = messages.RemoveMessage(context, messageId);
            return result;
        }

        [HttpPost]
        public int RemovePost(int postId, bool friendRemoving)
        {
            int result = 0;
            if(friendRemoving)
            {
                if(mainFormModels.user.IsAdmin == true)
                {
                    result = userProfileModel.RemovePost(context, postId);
                }
            }
            else
            {
                result = mainFormModels.RemovePost(context, postId);
            }
            return result;
        }


        [HttpPost]
        public int AdminRemovePost(int postId)
        {
            int result = 0;
            if(mainFormModels.adminGuest == true)
            {
                result = userProfileModel.RemovePost(context, postId);
            }
            return result;
        }

        [HttpPost]
        public IActionResult Comments(int id)
        { 
            commentsModel.user = mainFormModels.user;
            commentsModel.mainUserId = mainFormModels.mainUserId;
            commentsModel.GetComments(context, id);

            return View("Comments", commentsModel);
        }

        [HttpPost]
        public int AddComment(string message)
        {
            int result = commentsModel.AddComment(context, message);
            return result;
        }

        [HttpPost]
        public int RemoveComment(int commentId)
        {
            int result = commentsModel.RemoveComment(context, commentId);
            return result;
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
            userProfileModel.adminGuest = mainFormModels.user.IsAdmin;

            return View("Main", userProfileModel);
        }

        public IActionResult Friends(int id)
        {
            friendsModel.GetUserById(context, id);
            friendsModel.mainUserId = mainFormModels.user.UserId;

            return View(friendsModel);
        }

        public void DeleteFriend(int id)
        {
            friendsModel.DeleteFriend(context, id);
        }

        [HttpPost]
        public IActionResult VerifyUserLogin(string login, string password)
        {
            loginModel.Login = login;
            loginModel.Password = password;

            bool verifyResult = loginModel.Verify(context);
            if (verifyResult)
            {
                mainFormModels.GetUserInfo(context, -1, login, password);
                if(mainFormModels.user.IsBanned == true)
                {
                    return Content("Oi, you have been banned.");
                }
                return View("Main", mainFormModels);
            }
            return Content("Wrong login or password"); 
        }

        public int ChangeLike(int postId, bool friendLike)
        {
            int count = 0;
            if(friendLike)
            {
                userProfileModel.UpdatePostLike(context, postId, mainFormModels.mainUserId);
                count = userProfileModel.CountLikes(postId);
            }
            else
            {
                mainFormModels.UpdatePostLike(context, postId, mainFormModels.mainUserId);
                count = mainFormModels.CountLikes(postId);
            }
            return count;
        }

        [HttpPost]
        public (DbModels.User, int) AddPost(IFormFile file, string text)
        {
            string reference = FileManager.UploadImage(file);
            int lastPostId = -1;
            if (reference != null)
            {
                lastPostId = mainFormModels.AddPost(context, text, reference);
            }
            return (mainFormModels.user, lastPostId);
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
            if (!repeatCheck)
            {
                authModel.CreateUser(login, password, email, context);
                mainFormModels.GetUserInfo(context, -1, login, password);
                return View("Main", mainFormModels);
            }
            return Content("This user is already registered.");
        }
    }
}
