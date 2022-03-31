using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FaceSpam_social_media.Models
{
    public class FriendsModel
    {
        // this model is just a template
        // in some sprints it will be improved and supplemented

        public List<UserModel> userName = new List<UserModel>();

        public UserModel GetUser(string name)
        {
            UserModel result = null;
            foreach(UserModel user in userName)
            {
                if(user.getName == name)
                {
                    result = user;
                    break;
                }
            }
            return result;
        }
    }
}
