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
using FaceSpam_social_media.Infrastructure.Data;
using FaceSpam_social_media.Services;
using FaceSpam_social_media.Infrastructure.Repository;

namespace FaceSpam_social_media.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUserService _userService;

        public static Main mainFormModels = new Main();
        public static Main userProfileModel;
        public MVCDBContext context = new MVCDBContext();
        public static MessagesForm messages;
        public static FriendsViewModel friendsModel;
        public static PostCommentsModel commentsModel;
        public static LoginModel loginModel;
        public static SettingsModel settingsModel;
        public static AuthenticationModel authModel;
        public HomeController(ILogger<HomeController> logger, IUserService userService, IPostService postService,
            IRepository repository)
        {
            _logger = logger;
            _userService = userService;

            mainFormModels._repository = repository;
            //userProfileModel = new Main(repository);
            messages = new MessagesForm(repository);
            friendsModel = new FriendsViewModel(repository);
            commentsModel = new PostCommentsModel(repository);
            loginModel = new LoginModel(repository);
            settingsModel = new SettingsModel(/*repository*/);
            authModel = new AuthenticationModel(repository);
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Comments(int id)
        { 
            commentsModel.user = mainFormModels.user;
            commentsModel.post = context.Posts.Where(x => x.Id == id).FirstOrDefault();
            commentsModel.GetComments(context);

            return View("Comments", commentsModel);
        }

        public IActionResult AddComment(string message)
        {
            commentsModel.AddComment(context, message);

            return View("Comments", commentsModel);
        }

        public User GetUser() {

            return mainFormModels.user;
        }

        public IActionResult Messages()
        {
            messages.user = mainFormModels.user;
            messages.GetChats();
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
            userProfileModel.mainUserId = mainFormModels.user.Id;

            return View("Main", userProfileModel);
        }

        public IActionResult Friends(int id)
        {
            friendsModel.GetUserById(context, id);
            friendsModel.mainUserId = mainFormModels.user.Id;

            return View(friendsModel);
        }

        public void DeleteFriend(int id)
        {
            friendsModel.DeleteFriend(context, id);
        }

        [HttpPost]
        public IActionResult VerifyUserLogin(string login, string password)
        {
            bool verifyResult = loginModel.Verify(login, password);
            if (verifyResult)
            {
                mainFormModels.GetMainUserInfo(context, login, password);
                return View("Main", mainFormModels);
            }
            else { return Content("Wrong login or password"); }
        }

        public async Task<int> ChangeLike(int postId)
        {
            int count = await mainFormModels.UpdatePostLike(postId);
            return count;
        }

        [HttpPost]
        public async Task<int> AddPost(IFormFile file, string text)
        {
            string image_ref = null;
            if (file != null)
            {
                string extension = Path.GetExtension(file.FileName);
                if(extension != ".jpg" && extension != ".png")
                {
                    return (-1);
                }
                image_ref = "../Images/" + file.FileName;
                AddImageToPost(file);
            }
            int lastPostId = await mainFormModels.AddPost(text, image_ref);
            return (lastPostId);
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

        [HttpPost]
        public async Task<int> SendMessage(string textboxMessage)
        {
            int result = await messages.SendMessage(textboxMessage);
            return result;
        }

        public List<Message> GetChatMessages(int chatId)
        {
            messages.GetChatMessages(chatId);
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
            int id = mainFormModels.user.Id;
            _userService.UpdateUser(id, name, email, description);
            mainFormModels.UpdateMainUserInfo(settingsModel.user.Name, settingsModel.user.Email, settingsModel.user.Description);

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
                return Content("This user is already registered.");
            }
            else
            {
                string imageReference = "../Images/DefaultUserImage.png";
                await _userService.AddUser(login, password, email, imageReference);

                mainFormModels.GetMainUserInfo(context, login, password);
                return View("Main", mainFormModels);
            }
        }

    }
}
