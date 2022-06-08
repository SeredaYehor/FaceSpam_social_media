using FaceSpam_social_media.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.AspNetCore.Http;
using FaceSpam_social_media.DbModels;
using System.Linq;

namespace FaceSpam_social_media.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public static Main mainFormModels = new Main();
        public mydbContext context = new mydbContext();
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

        [HttpPost]
        public List<User> SelectUsers()
        {
            List<User> users = messages.SelectAllUsers(context);
            return users;
        }

        public IActionResult Admin()
        {
            if(usersManagment.Init(context, mainFormModels.executor))
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
        public int RemovePost(int postId)
        {
            int result = 0;
            if(mainFormModels.executor.IsAdmin == true)
            {
                result = mainFormModels.RemovePost(context, postId);
            }
            return result;
        }


        [HttpPost]
        public int AdminRemovePost(int postId)
        {
            int result = 0;
            if(mainFormModels.executor.IsAdmin == true)
            {
                result = mainFormModels.RemovePost(context, postId);
            }
            return result;
        }

        [HttpPost]
        public IActionResult Comments(int id)
        {
            commentsModel.user = mainFormModels.executor;
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

        public User GetUser() {

            return mainFormModels.executor;
        }

        public IActionResult Messages(int current = 0)
        {
            messages.user = mainFormModels.executor;
            messages.GetChatMessages(context, current);
            messages.GetChats(context);
            return View(messages);
        }

        [HttpPost]
        public int RemoveChatMember(int memberId)
        {
            int members = messages.RemoveChatMember(context, memberId);
            return members;
        }

        [HttpPost]
        public int AddMember(int memberId)
        {
            int members = messages.AddMember(context, memberId);
            return members;
        }

        public IActionResult GetChat(int chatId)
        {
            messages.user = mainFormModels.executor;
            messages.SelectChat(context, chatId);
            return View(messages);
        }

        [HttpPost]
        public IActionResult Main()
        {
            mainFormModels.user = mainFormModels.executor;
            mainFormModels.GetFriends(context);
            return View(mainFormModels);
        }

        [HttpPost]
        public IActionResult UserProfile(int id) 
        {
            mainFormModels.GetUserInfo(context, true, id);
            return View("Main", mainFormModels);
        }

        public IActionResult Friends(int id)
        {
            friendsModel.GetUserById(context, id);
            friendsModel.mainUserId = mainFormModels.executor.UserId;
            return View(friendsModel);
        }

        public void DeleteFriend(int id)
        {
            friendsModel.DeleteFriend(context, id);
        }

        public int ChangeLike(int postId, bool friendLike)
        {
            int count = 0;
            mainFormModels.UpdatePostLike(context, postId);
            count = mainFormModels.CountLikes(postId);
            return count;
        }

        [HttpPost]
        public Chat CreateGroup(string chatName, string chatDescription, string members, IFormFile file)
        {
            List<int> listMembers = members.Split(',').Select(int.Parse).ToList();
            string reference = FileManager.UploadImage(file);
            Chat result = messages.CreateGroup(context, chatName, chatDescription, listMembers, reference);
            return result;
        }

        public void QuitGroup()
        {
            messages.QuitGroup(context);
        }

        [HttpPost]
        public void DeleteGroup(int groupId)
        {
            messages.DeleteGroup(context, groupId);
        }

        [HttpPost]
        public (User, int) AddPost(IFormFile file, string text)
        {
            int lastPostId = -1;
            string reference = FileManager.UploadImage(file);
            lastPostId = mainFormModels.AddPost(context, text, reference);
            return (mainFormModels.user, lastPostId);
        }

        public (User, int) SendMessage(string textboxMessage)
        {
            int id = messages.SendMessage(context, textboxMessage);
            return (messages.user, id);
        }

        public (List<Message>, Chat, int) GetChatMessages(int chatId)
        {
            Chat chat = messages.GetChatMessages(context, chatId);
            return (messages.chatMessages, chat, messages.members.Count);
        }
        
        public List<User> GetChatUsers()
        {
            return messages.members;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        
        public IActionResult Groups()
        {
            messages.user = mainFormModels.executor;
            messages.GetChats(context);
            return View(messages);
        }

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Settings()
        {
            settingsModel.user = mainFormModels.executor;
            return View(settingsModel);
        }

        public IActionResult ChangeUserInfo(string email, string name, string description)
        {
            settingsModel.ChangeUserInfo(context, email, name, description);
            mainFormModels.UpdateData(settingsModel.user);
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
                mainFormModels.GetUserInfo(context, false, -1, login, password);
                return View("Main", mainFormModels);
            }
            return Content("This user is already registered.");
        }

        [HttpPost]
        public IActionResult VerifyUserLogin(string login, string password)
        {
            loginModel.Login = login;
            loginModel.Password = password;

            bool verifyResult = loginModel.Verify(context);
            if (verifyResult)
            {
                mainFormModels.GetUserInfo(context, false, -1, login, password);
                if (mainFormModels.user.IsBanned == true)
                {
                    return Content("Oi, you have been banned.");
                }
                return View("Main", mainFormModels);
            }
            return Content("Wrong login or password");
        }
    }
}
