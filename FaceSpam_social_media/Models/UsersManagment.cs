using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FaceSpam_social_media.Infrastructure.Data;
using FaceSpam_social_media.Infrastructure.Repository;
using FaceSpam_social_media.Services;

namespace FaceSpam_social_media.Models
{
    public class UsersManagment
    {
        public User admin;
        public List<User> users;

        public UsersManagment() {}

        public bool Init(IUserService userService, User user)
        {
            bool result = false;
            if (user.IsAdmin == true)
            {
                admin = user;
                users = userService.GetAllUsers(admin.Id);
                result = true;
            }
            return result;
        }
    }
}
