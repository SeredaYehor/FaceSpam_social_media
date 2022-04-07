using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace FaceSpam_social_media.Models
{
    public class Post
    {
        public Post(string name, string text, string time, string image, string post)
        {
            userName = name;
            postText = text;
            postCreating = time;
            userImage = image;
            postImage = post;
        }

        private string userName;
        private string postText;
        private string userImage;
        private string postImage;

        private string postCreating;

        public List<PostComment> postComments = new List<PostComment>();
        public string getUser
        {
            get => userName;
        }

        public string getMessage
        {
            get => postText;
        }

        public string getPostImage
        {
            get => postImage;
        }

        public string getTime
        {
            get => postCreating;
        }

        public string getUserImage
        {
            get => userImage;
        }
    }
}
