using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace FaceSpam_social_media.Models
{
    public class Post
    {
        public Post(string name, string text, DateTime time)
        {
            userName = name;
            postText = text;
            postCreating = time;
        }

        private string userName;
        private string postText;

        private DateTime postCreating = new DateTime();

        public List<PostComment> postComments = new List<PostComment>();
        public string getUser
        {
            get => userName;
        }

        public string getMessage
        {
            get => postText;
        }

        public DateTime getTime
        {
            get => postCreating;
        }
    }
}
