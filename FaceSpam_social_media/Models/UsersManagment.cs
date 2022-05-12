using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FaceSpam_social_media.DbModels;

namespace FaceSpam_social_media.Models
{
    public class UsersManagment
    {
        public User admin;
        public List<User> users;

        public bool Init(mydbContext context, User user)
        {
            bool result = false;
            if(user.IsAdmin == true)
            {
                admin = user;
                GetAllUsers(context);
                result = true;
            }
            return result;
        }
        public void GetAllUsers(mydbContext context)
        {
            users = context.Users.Where(x => x.UserId != admin.UserId).ToList();
        }

        public int UpdateStatus(mydbContext context, int userId)
        {
            int result = -1;
            User target = users.Where(x => x.UserId == userId).First();
            bool? status = target.IsBanned;
            target.IsBanned = !status;
            context.Users.Update(target);
            result = context.SaveChanges();
            return result;
        }
    }
}
