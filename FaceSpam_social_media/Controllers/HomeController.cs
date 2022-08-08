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
using System;
using Microsoft.AspNetCore.SignalR;

namespace FaceSpam_social_media.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        #region Database Services
        private readonly IUserService _userService;
        private readonly ILikeService _likeService;
        private readonly IPostService _postService;
        private readonly IChatService _chatService;
        private readonly IChatMemberService _chatMemberService;
        private readonly IMessageService _messageService;
        private readonly IFollowService _friendService;
        #endregion

        public static Dictionary<int, string> ImageStorage = new Dictionary<int, string>();

        public HomeController(ILogger<HomeController> logger, IUserService userService,
            IChatMemberService chatMemberService, IChatService chatService,
            ILikeService likeService, IPostService postService,
            IMessageService messageService, IFollowService friendService)
        {
            #region Initialization
            _logger = logger;
            _userService = userService;
            _likeService = likeService;
            _postService = postService;
            _chatService = chatService;
            _chatMemberService = chatMemberService;
            _messageService = messageService;
            _friendService = friendService;
            #endregion
        }

        public IActionResult Index()
        {
            return View();
        }

        public User GetUser(int executorId)
        {
            return _userService.GetUser(executorId);
        }

        public string[] IdSpliter(string idCollection)
        {
            return idCollection.Split("-");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        #region Admin form functions
        public IActionResult Admin(int executorId)
        {
            UsersManagment usersManagment = new UsersManagment();
            usersManagment.admin = _userService.GetUser(executorId);

            if (usersManagment.Init(_userService, usersManagment.admin))
            {
                return View(usersManagment);
            }
            return Content("Cannot get this page.");
        }

        [HttpPost]
        public async Task<int> UpdateUserStatus(int userId)
        {
            int result = await _userService.UpdateStatus(userId);
            return result;
        }
        #endregion

        #region Main form functions

        public IActionResult Main(int executorId)
        {
            Main userModel = new Main();

            userModel.GetUserInfo(_userService, _postService, _likeService, _friendService, false, executorId);
            userModel.executor = userModel.user;

            return View(userModel);
        }

        [HttpPost]
        public async Task<int> RemovePost(int postId)
        {
            int result = 0;
            result = await _postService.RemovePost(postId);
            return result;
        }

        [HttpPost]
        public IActionResult UserProfile(string id)
        {
            Main profileModel = new Main();

            string[] ids = IdSpliter(id);
            int executorId = Convert.ToInt32(ids[0]);

            profileModel.executor = _userService.GetUser(executorId);
            profileModel.GetUserInfo(_userService, _postService, _likeService, _friendService, true, Convert.ToInt32(ids[1]));
            return View("Main", profileModel);
        }

        [HttpPost]
        public async Task<(User, int)> AddPost(IFormFile file, int id, string text)
        {
            int lastPostId = -1;
            string reference = FileManager.UploadImage(file);
            lastPostId = await _postService.AddPost(id, text, reference);

            User user = _userService.GetUser(id);

            return (user, lastPostId);
        }

        public async Task<int> ChangeLike(int postId, int executorId)
        {
            int count = 0;
            count = await _likeService.UpdatePostLike(executorId, postId);
            return count;
        }
        #endregion

        #region Comments form functions
        [HttpPost]
        public IActionResult Comments(string id)
        {
            PostCommentsModel commentsModel = new PostCommentsModel();

            string[] ids = IdSpliter(id);

            commentsModel.user = _userService.GetUser(Convert.ToInt32(ids[0]));
            commentsModel.GetComments(Convert.ToInt32(ids[1]), _messageService, _userService, _postService);
            return View("Comments", commentsModel);
        }

        [HttpPost]
        public async Task<int> AddComment(string message, int executorId, int postId)
        {
            Message result = await _messageService
                .AddMessage(postId, executorId, message, true);

            User executor = _userService.GetUser(executorId);

            return result.Id;
        }

        [HttpPost]
        public async Task<int> RemoveComment(int commentId)
        {
            Message result = await _messageService.DeleteMessage(commentId);
            return result.Id;
        }

        #endregion

        #region Messages form functions

        [HttpPost]
        public IActionResult Messages(int executorId, int current = 0)
        {
            MessagesForm messages = new MessagesForm();

            messages.user = _userService.GetUser(executorId);
            messages.GetChatMessages(_chatMemberService, _messageService, _chatService, current);
            messages.chats = _chatService.GetChats(executorId);
            return View(messages);
        }

        [HttpPost]
        public async Task<int> RemoveChatMember(int memberId, int chatId)
        {
            int members = await _chatMemberService.RemoveChatMember(
                chatId, memberId);
            return members;
        }

        [HttpPost]
        public async Task<int> AddMember(int memberId, int chatId)
        {
            int members = await _chatMemberService.AddChatMember(
                chatId, memberId);
            return members;
        }

        public IActionResult GetChat(int executorId, int chatId)
        {
            MessagesForm chat = new MessagesForm();

            chat.user = _userService.GetUser(executorId);
            chat.GetChatMessages(_chatMemberService, _messageService, _chatService, chatId);
            return View(chat);
        }

        [HttpPost]
        public List<User> SelectUsers(int exceptId)
        {
            List<User> users = _userService.SelectAllUsers(exceptId);
            return users;
        }

        public async Task<(User, int)> SendMessage(string textboxMessage, int chatId, int executorId)
        {
            Message result = await _messageService.AddMessage(chatId, executorId, textboxMessage);
            return (_userService.GetUser(executorId), result.Id);
        }

        public (List<Message>, Chat, int) GetChatMessages(int chatId)
        {
            MessagesForm chat = new MessagesForm();

            chat.GetChatMessages(_chatMemberService, _messageService, _chatService, chatId);
            return (chat.chatMessages,
                chat.selectedChat, chat.members.Count());
        }

        public List<User> GetChatUsers(int chatId)
        {
            List<User> result = _chatMemberService.GetChatMembers(chatId);
            return result;
        }

        [HttpPost]
        public async Task<int> RemoveMessage(int messageId)
        {
            Message result = await _messageService.DeleteMessage(messageId);

            return result.Id;
        }
        #endregion

        #region Followings form functions
        public IActionResult Followings(string id)
        {
            FollowsViewModel followsModel = new FollowsViewModel();

            string[] ids = IdSpliter(id);

            followsModel.executor = Convert.ToInt32(ids[0]);
            followsModel.user = _userService.GetUser(Convert.ToInt32(ids[1]));
            followsModel.follows = _friendService.GetFollowingUsers(Convert.ToInt32(ids[1]));
            followsModel.friendPage = true;

            return View(followsModel);
        }

        public IActionResult UserList(string id)
        {
            FollowsViewModel followsModel = new FollowsViewModel();

            string[] ids = IdSpliter(id);

            followsModel.user = _userService.GetUser(Convert.ToInt32(ids[1]));

            followsModel.executor = Convert.ToInt32(ids[0]);
            followsModel.allUsers = _userService.GetAllUsers(Convert.ToInt32(ids[0]));
            followsModel.follows = _friendService.GetFollowingUsers(Convert.ToInt32(ids[0]));
            followsModel.friendPage = false;

            return View("Followings", followsModel);
        }

        public async Task DeleteFollow(int userId, int executorId)
        {
            await _friendService.RemoveFollowing(executorId, userId);
        }

        public async Task AddFollow(int userId, int executorId)
        {
            await _friendService.Addfollowing(executorId, userId);
        }
        #endregion

        #region Groups panel funct
        [HttpPost]
        public async Task<Chat> CreateGroup(string chatName, string chatDescription, string members, int executorId, IFormFile file)
        {
            List<int> listMembers = members.Split(',').Select(int.Parse).ToList();
            string reference = FileManager.UploadImage(file);
            Chat result = await _chatService.CreateGroup(executorId,
                chatName, chatDescription, listMembers, reference);
            return result;
        }

        public async Task QuitGroup(int executorId, int chatId)
        {
            await _chatService.QuitGroup(chatId,
                executorId);
        }

        [HttpPost]
        public async Task DeleteGroup(int groupId)
        {
            await _chatService.DeleteGroup(groupId);
        }

        public IActionResult Groups(int executorId)
        {
            MessagesForm messages = new MessagesForm();

            messages.user = _userService.GetUser(executorId);
            messages.chats = _chatService.GetChats(executorId);

            return View(messages);
        }
        #endregion

        #region Settings form functions
        public IActionResult Settings(int executorId)
        {
            SettingsModel settingsModel = new SettingsModel();

            settingsModel.user = _userService.GetUser(executorId);
            return View(settingsModel);
        }

        public void GetPhotoUrl(IFormFile file, int executorId)
        {
            string imagePath = FileManager.UploadImage(file);
            ImageStorage.Add(executorId, imagePath);
        }

        [HttpPost]
        public async Task<IActionResult> ChangeUserInfo(string email, string name, string description, int executorId)
        {
            await _userService.UpdateUser(executorId, name,
                email, description, ImageStorage[executorId]);

            ImageStorage.Remove(executorId);

            return RedirectToAction("Main", new { executorId = executorId });
        }
        #endregion

        #region Authentication form functions
        public IActionResult Authentication()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> VerifyUserAuthentication(string login, string password, string email)
        {
            Main mainFormModel = new Main();
            ErrorPageModel errorModel = new ErrorPageModel();

            bool repeatCheck = _userService.CheckCopy(login);
            if (!repeatCheck)
            {
                string imageReference = "../Images/DefaultUserImage.png";
                await _userService.AddUser(login, password, email, imageReference);
                mainFormModel.GetUserInfo(_userService, _postService, _likeService, _friendService, false, -1, login, password);
                return RedirectToAction("Main", new { executorId = mainFormModel.user.Id });
            }
            else
            {
                errorModel.Error = "This data is already in use";
                return View("ErrorPage", errorModel);
            }
        }
        #endregion

        #region Login form functions
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]

        [HttpPost]
        public IActionResult VerifyUserLogin(string login, string password)
        {

            Main userModel = new Main();
            ErrorPageModel errorModel = new ErrorPageModel();

            bool verifyResult = _userService.Verify(login, password);
            if (verifyResult)
            {
                userModel.GetUserInfo(_userService, _postService, _likeService, _friendService, false, -1, login, password);

                if (userModel.user.IsBanned == true)
                {
                    errorModel.Error = "Oi, you have been banned";
                    return View("ErrorPage", errorModel);
                }
                return RedirectToAction("Main", "Home", new { executorId = userModel.user.Id });
            }
            else
            {
                errorModel.Error = "Wrong login or password";
                return View("ErrorPage", errorModel);
            }
        }

        public IActionResult ErrorLoginPage()
        {
            return View("ErrorLoginPage");
        }
        #endregion
    }
}
