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

        #region Database Services
        private readonly IUserService _userService;
        private readonly ILikeService _likeService;
        private readonly IPostService _postService;
        private readonly IChatService _chatService;
        private readonly IChatMemberService _chatMemberService;
        private readonly IMessageService _messageService;
        private readonly IFollowService _friendService;
        #endregion

        #region ViewModels
        public static Main mainFormModels = new Main();
        public static MessagesForm messages = new MessagesForm();
        public static FollowsViewModel followsModel = new FollowsViewModel();
        public static PostCommentsModel commentsModel = new PostCommentsModel();
        public LoginModel loginModel = new LoginModel();
        public static SettingsModel settingsModel = new SettingsModel();
        public AuthenticationModel authModel = new AuthenticationModel();
        public UsersManagment usersManagment = new UsersManagment();
        public ErrorPageModel errorModel = new ErrorPageModel();
        #endregion

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

        public User GetUser()
        {
            return mainFormModels.executor;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        #region Admin form functions
        public IActionResult Admin()
        {
            if (usersManagment.Init(_userService, mainFormModels.executor))
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
        public IActionResult Main()
        {
            mainFormModels.user = mainFormModels.executor;
            mainFormModels.GetUserInfo(_userService, _postService, _likeService, _friendService, false, mainFormModels.executor.Id);
            followsModel.GetMainFormData(mainFormModels);

            return View(mainFormModels);
        }

        [HttpPost]
        public async Task<int> RemovePost(int postId)
        {
            int result = 0;
            result = await _postService.RemovePost(mainFormModels.executor.Id,
                    postId);
            return result;
        }


        [HttpPost]
        public async Task<int> AdminRemovePost(int postId)
        {
            int result = 0;
            if (mainFormModels.executor.IsAdmin == true)
            {
                result = await _postService.RemovePost(mainFormModels.executor.Id,
                    postId);
            }
            return result;
        }

        [HttpPost]
        public IActionResult UserProfile(int id)
        {
            mainFormModels.GetUserInfo(_userService, _postService, _likeService, _friendService, true, id);
            return View("Main", mainFormModels);
        }

        [HttpPost]
        public async Task<(User, int)> AddPost(IFormFile file, string text)
        {
            int lastPostId = -1;
            string reference = FileManager.UploadImage(file);
            lastPostId = await _postService.AddPost(mainFormModels.executor.Id, text, reference);
            return (mainFormModels.user, lastPostId);
        }

        public async Task<int> ChangeLike(int postId)
        {
            int count = 0;
            count = await _likeService.UpdatePostLike(mainFormModels.executor.Id, postId);
            return count;
        }
        #endregion

        #region Comments form functions
        [HttpPost]
        public IActionResult Comments(int id)
        {
            commentsModel.user = mainFormModels.executor;
            commentsModel.GetComments(id, _messageService, _userService, mainFormModels);
            return View("Comments", commentsModel);
        }

        [HttpPost]
        public async Task<int> AddComment(string message)
        {
            Message result = await _messageService
                .AddMessage(commentsModel.post.Id, commentsModel.user.Id, message, true);

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
        public IActionResult Messages(int current = 0)
        {
            messages.user = mainFormModels.executor;
            messages.GetChatMessages(_chatMemberService, _messageService, current);
            messages.chats = _chatService.GetChats(mainFormModels.executor.Id);
            return View(messages);
        }
        
        [HttpPost]
        public async Task<int> RemoveChatMember(int memberId)
        {
            int members =  await _chatMemberService.RemoveChatMember(
                messages.selectedChat.Id,
                memberId);
            return members;
        }
        
        [HttpPost]
        public async Task<int> AddMember(int memberId)
        {
            int members = await _chatMemberService.AddChatMember(
                messages.selectedChat.Id, 
                memberId);
            return members;
        }
        
        public IActionResult GetChat(int chatId)
        {
            messages.user = mainFormModels.executor;
            messages.GetChatMessages(_chatMemberService, _messageService, chatId);
            return View(messages);
        }

        [HttpPost]
        public List<User> SelectUsers(int exceptId)
        {
            List<User> users = _userService.SelectAllUsers(exceptId);
            return users;
        }

        public async Task<(User, int)> SendMessage(string textboxMessage)
        {
            Message result = await _messageService.AddMessage(messages.currentChat, messages.user.Id, textboxMessage);
            return (messages.user, result.Id);
        }

        public (List<Message>, Chat, int) GetChatMessages(int chatId)
        {
            messages.GetChatMessages(_chatMemberService, _messageService, chatId);
            return (messages.chatMessages,
                messages.selectedChat, messages.members.Count());
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
        public IActionResult Followings(int id)
        {
            followsModel.user = _userService.GetUser(id);
            followsModel.follows = _friendService.GetFollowingUsers(id);
            followsModel.allUsers.Clear();
            followsModel.friendPage = true;

            return View(followsModel);
        }

        public IActionResult UserList()
        {
            followsModel.allUsers = _userService.GetAllUsers(followsModel.executor);
            followsModel.follows = _friendService.GetFollowingUsers(followsModel.executor);
            followsModel.friendPage = false;

            return View("Followings", followsModel);
        }
        
        public async Task DeleteFollow(int id)
        {
            await _friendService.RemoveFollowing(followsModel.executor, id);
        }

        public async Task AddFollow(int id)
        {
            await _friendService.Addfollowing(followsModel.executor, id);
        }
        #endregion

        #region Groups panel funct
        [HttpPost]
        public async Task<Chat> CreateGroup(string chatName, string chatDescription, string members, IFormFile file)
        {
            List<int> listMembers = members.Split(',').Select(int.Parse).ToList();
            string reference = FileManager.UploadImage(file);
            Chat result = await _chatService.CreateGroup(mainFormModels.executor.Id,
                chatName, chatDescription, listMembers, reference);
            messages.chats.Add(result);
            return result;
        }

        public async Task QuitGroup()
        {
            await _chatService.QuitGroup(messages.selectedChat.Id,
                messages.user.Id);
        }

        [HttpPost]
        public async Task DeleteGroup(int groupId)
        {
            await _chatService.DeleteGroup(groupId);
        }

        public IActionResult Groups()
        {
            messages.user = mainFormModels.executor;
            messages.chats = _chatService.GetChats(mainFormModels.executor.Id);
            return View(messages);
        }
        #endregion

        #region Settings form functions
        public IActionResult Settings()
        {
            settingsModel.user = mainFormModels.executor;
            return View(settingsModel);
        }

        public void GetPhotoUrl(IFormFile file)
        {
            settingsModel.imageReference = FileManager.UploadImage(file);
        }
        
        [HttpPost]
        public async Task<IActionResult> ChangeUserInfo(string email, string name, string description)
        {
            await _userService.UpdateUser(mainFormModels.user.Id, name, 
                email, description, settingsModel.imageReference);
            mainFormModels.UpdateUserInfo(name, email, description, settingsModel.imageReference);
            mainFormModels.UpdateData(settingsModel.user);

            return RedirectToAction("Main");
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
            bool repeatCheck = _userService.CheckCopy(login);
            if (!repeatCheck)
            {
                string imageReference = "../Images/DefaultUserImage.png";
                await _userService.AddUser(login, password, email, imageReference);
                mainFormModels.GetUserInfo(_userService, _postService, _likeService, _friendService, false, -1, login, password);
                return RedirectToAction("Main");
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
        public IActionResult VerifyUserLogin(string login, string password)
        {
            loginModel.Login = login;
            loginModel.Password = password;

            bool verifyResult = _userService.Verify(login, password);
            if (verifyResult)
            {
                mainFormModels.GetUserInfo(_userService, _postService, _likeService, _friendService, false, -1, login, password);
                if (mainFormModels.user.IsBanned == true)
                {
                    errorModel.Error = "Oi, you have been banned";
                    return View("ErrorPage", errorModel);
                }
                return RedirectToAction("Main");
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
        #endregion
    }
}
