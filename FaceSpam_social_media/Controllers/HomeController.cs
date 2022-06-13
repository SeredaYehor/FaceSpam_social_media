using FaceSpam_social_media.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using FaceSpam_social_media.Infrastructure.Data;
using FaceSpam_social_media.Services;
using FaceSpam_social_media.Infrastructure.Repository;
using System.Threading.Tasks;
using System.Linq;

namespace FaceSpam_social_media.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUserService _userService;

        public static Main mainFormModels = new Main();
        public static Main userProfileModel = new Main();
        public static MessagesForm messages = new MessagesForm();
        public static FriendsViewModel friendsModel = new FriendsViewModel();
        public static PostCommentsModel commentsModel = new PostCommentsModel();
        public static LoginModel loginModel = new LoginModel();
        public static SettingsModel settingsModel = new SettingsModel();
        public static AuthenticationModel authModel = new AuthenticationModel();
        public static UsersManagment usersManagment = new UsersManagment();
        public static ErrorPageModel errorModel = new ErrorPageModel();

        public HomeController(ILogger<HomeController> logger, IUserService userService, IPostService postService,
            IRepository repository)
        {
            _logger = logger;
            _userService = userService;

            mainFormModels._repository = repository;
            userProfileModel._repository = repository;
            messages._repository = repository;
            friendsModel._repository = repository;
            commentsModel._repository = repository;
            loginModel._repository = repository;
            settingsModel._repository = repository; 
            authModel._repository = repository;
            usersManagment._repository = repository;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Admin()
        {
            if (usersManagment.Init(mainFormModels.executor))
            {
                return View(usersManagment);
            }
            return Content("Cannot get this page.");
        }

        [HttpPost]
        public async Task<int> UpdateUserStatus(int userId)
        {
            int result = await usersManagment.UpdateStatus(userId);
            return result;
        }

        [HttpPost]
        public async Task<int> RemoveMessage(int messageId)
        {
            int result = await messages.RemoveMessage(messageId);
            return result;
        }

        [HttpPost]
        public async Task<int> RemovePost(int postId)
        {
            int result = 0;
            if (mainFormModels.executor.IsAdmin == true)
            {
                result = await mainFormModels.RemovePost(postId);
            }
            return result;
        }


        [HttpPost]
        public async Task<int> AdminRemovePost(int postId)
        {
            int result = 0;
            if (mainFormModels.executor.IsAdmin == true)
            {
                result = await mainFormModels.RemovePost(postId);
            }
            return result;
        }

        [HttpPost]
        public IActionResult Comments(int id)
        {
            commentsModel.user = mainFormModels.executor;
            commentsModel.GetComments(id);
            return View("Comments", commentsModel);
        }

        [HttpPost]
        public async Task<int> AddComment(string message)
        {
            int result = await commentsModel.AddComment(message);
            return result;
        }

        [HttpPost]
        public async Task<int> RemoveComment(int commentId)
        {
            int result = await commentsModel.RemoveComment(commentId);
            return result;
        }

        [HttpPost]
        public List<User> SelectUsers()
        {
            List<User> users = messages.SelectAllUsers();
            return users;
        }

        public User GetUser()
        {
            return mainFormModels.executor;
        }

        public IActionResult Messages(int current = 0)
        {
            messages.user = mainFormModels.executor;
            messages.GetChatMessages(current);
            messages.GetChats();
            return View(messages);
        }
        
        [HttpPost]
        public async Task<int> RemoveChatMember(int memberId)
        {
            int members =  await messages.RemoveChatMember(memberId);
            return members;
        }
        
        [HttpPost]
        public async Task<int> AddMember(int memberId)
        {
            int members = await messages.AddMember(memberId);
            return members;
        }
        
        public IActionResult GetChat(int chatId)
        {
            messages.user = mainFormModels.executor;
            messages.SelectChat(chatId);
            return View(messages);
        }
        
        [HttpPost]
        public IActionResult Main()
        {
            mainFormModels.user = mainFormModels.executor;
            mainFormModels.GetFriends();
            friendsModel.GetMainFormData(mainFormModels);

            return View(mainFormModels);
        }

        [HttpPost]
        public IActionResult UserProfile(int id)
        {
            mainFormModels.GetUserInfo(true, id);
            return View("Main", mainFormModels);
        }

        public IActionResult Friends(int id)
        {
            friendsModel.GetUserById(id);
            friendsModel.allUsers.Clear();
            friendsModel.friendPage = true;

            return View(friendsModel);
        }

        public IActionResult UserList()
        {
            friendsModel.GetAllUsers();
            friendsModel.friendPage = false;

            return View("Friends", friendsModel);
        }
        
        public async Task<int> DeleteFriend(int id)
        {
            int result = await friendsModel.DeleteFriend(id);
            mainFormModels.GetFriends();
            return result;
        }

        public async Task<int> AddFriend(int id)
        {
            int result = await friendsModel.AddFriend(id);
            mainFormModels.GetFriends();
            return result;
        }

        public async Task<int> ChangeLike(int postId)
        {
            int count = 0;
            await mainFormModels.UpdatePostLike(postId);
            count = mainFormModels.CountLikes(postId);
            return count;
        }
        
        [HttpPost]
        public async Task<Chat> CreateGroup(string chatName, string chatDescription, string members, IFormFile file)
        {
            List<int> listMembers = members.Split(',').Select(int.Parse).ToList();
            string reference = FileManager.UploadImage(file);
            Chat result = await messages.CreateGroup(chatName, chatDescription, listMembers, reference);
            return result;
        }

        public async Task QuitGroup()
        {
            await messages.QuitGroup();
        }

        [HttpPost]
        public async Task DeleteGroup(int groupId)
        {
            await messages.DeleteGroup(groupId);
        }
        
        [HttpPost]
        public async Task<(User, int)> AddPost(IFormFile file, string text)
        {
            int lastPostId = -1;
            string reference = FileManager.UploadImage(file);
            lastPostId = await mainFormModels.AddPost(text, reference);
            return (mainFormModels.user, lastPostId);
        }

        public async Task<(User, int)> SendMessage(string textboxMessage)
        {
            int id = await messages.SendMessage(textboxMessage);
            return (messages.user, id);
        }

        public (List<Message>, Chat, int) GetChatMessages(int chatId)
        {
            messages.GetChatMessages(chatId);
            return (messages.chatMessages, 
                messages.selectedChat, messages.members.Count);
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
            messages.GetChats();
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

        public async Task<IActionResult> ChangeUserInfo(string email, string name, string description)
        {
            await _userService.UpdateUser(mainFormModels.user.Id, name, email, description);

            mainFormModels.UpdateUserInfo(name, email, description);
            mainFormModels.UpdateData(settingsModel.user);
            commentsModel.user = mainFormModels.user;
            friendsModel.user = mainFormModels.user;

            return View("Main", mainFormModels);
        }

        public IActionResult Authentication()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> VerifyUserAuthentication(string login, string password, string email)
        {
            bool repeatCheck = authModel.Verify(login, email);
            if (repeatCheck)
            {
                string imageReference = "../Images/DefaultUserImage.png";
                await _userService.AddUser(login, password, email, imageReference);

                mainFormModels.GetMainUserInfo(login, password);
                return View("Main", mainFormModels);
            }
            else
            {
                errorModel.Error = "This data is already in use";
                return View("ErrorPage", errorModel);
            }
        }

        [HttpPost]
        public IActionResult VerifyUserLogin(string login, string password)
        {
            loginModel.Login = login;
            loginModel.Password = password;

            bool verifyResult = loginModel.Verify(login, password);
            if (verifyResult)
            {
                mainFormModels.GetUserInfo( false, -1, login, password);
                mainFormModels.user = mainFormModels.executor;
                mainFormModels.GetFriends();
                friendsModel.GetMainFormData(mainFormModels);

                if (mainFormModels.user.IsBanned == true)
                {
                    errorModel.Error = "Oi, you have been banned";
                    return View("ErrorPage", errorModel);
                }
                return View("Main", mainFormModels);
            }
            else
            {
                errorModel.Error = "Wrong login or password";
                return View("ErrorPage", errorModel);
            }
        }
        public IActionResult ErrorLoginPage()
        {
            return View("ErrorLoginPage", errorModel);
        }

    }
}