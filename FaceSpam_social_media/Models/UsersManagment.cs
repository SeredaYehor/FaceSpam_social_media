using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FaceSpam_social_media.Models
{
    public class UsersManagment
    {
        public DbModels.User admin;
        public List<DbModels.User> users;

        public bool Init(DbModels.mydbContext context, DbModels.User user)
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
        public void GetAllUsers(DbModels.mydbContext context)
        {
            users = context.Users.Where(x => x.UserId != admin.UserId).ToList();
        }

        public int UpdateStatus(DbModels.mydbContext context, int userId)
        {
            int result = -1;
            DbModels.User target = users.Where(x => x.UserId == userId).First();
            bool? status = target.IsBanned;
            target.IsBanned = !status;
            context.Users.Update(target);
            result = context.SaveChanges();
            return result;
        }
    }
}
