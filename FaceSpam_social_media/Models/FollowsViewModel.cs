using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FaceSpam_social_media.Infrastructure.Data;
using FaceSpam_social_media.Infrastructure.Repository;
using FaceSpam_social_media.Services;

namespace FaceSpam_social_media.Models
{
    public class FollowsViewModel
    {
        public User user;
        public List<User> follows = new List<User>();
        public List<User> allUsers = new List<User>();
        public int executor;

        public bool friendPage;

        public FollowsViewModel()
        {
        }

        public string IsFollowed(int userId)
        {
            string result = "Follow";
            foreach(var user in follows)
            {
                if(user.Id == userId)
                {
                    result = "Remove";
                    break;
                }
            }         

            return result;
        }

        public void GetMainFormData(Main mainModel)
        {
            follows = mainModel.follows;
            executor = mainModel.executor.Id;
        }
    }
}
