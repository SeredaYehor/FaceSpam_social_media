using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FaceSpam_social_media.Models
{
    public class Main
    {
        public User user;
        public List<Post> posts = new List<Post>();
        public int GetPostIndex(int postId)
        {
            int result = -1;
            for(int index = 0; index < posts.Count; index++)
            {
                if(posts[index].ID == postId)
                {
                    result = index;
                    break;
                }

            }
            return result;
        }
    }
}
