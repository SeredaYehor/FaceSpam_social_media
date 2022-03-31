using FaceSpam_social_media.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace FaceSpam_social_media.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        protected static FriendsModel friends = new FriendsModel();

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            friends.userName.Add(new UserModel("Dorenskyi Aleksandr", "Cyberdemon.png"));
            friends.userName.Add(new UserModel("Mitropolit Vadim", "Vadim.png"));
            friends.userName.Add(new UserModel("Elon Mask", "Mask.jpg"));
            friends.userName.Add(new UserModel("Kenoby", "Kenoby.jpg"));

            return View();
        }

        public IActionResult Comments()
        {
            return View();
        }

        public IActionResult Friends()
        {
            return View(friends);
        }

        [HttpPost]
        public IActionResult DeleteFriend(string name)
        {
            List<UserModel> temp = new List<UserModel>(friends.userName);

            foreach (var friend in temp)
            {
                if (friend.getName.Equals(name))
                {
                    friends.userName.Remove(friend);
                }
            }
            temp.Clear();

            return View("Friends", friends);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
