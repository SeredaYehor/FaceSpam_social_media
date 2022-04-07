using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FaceSpam_social_media.Models
{
    public class PostComment
    {
        private string userName;
        private string postText;
        private string commentCreated;
        private string userImage;

        public string getUser
        {
            get => userName;
        }

        public string getMessage
        {
            get => postText;
        }

        public string getTime
        {
            get => commentCreated;
        }

        public string getUserImage
        {
            get => userImage;
        }

        public PostComment(string name, string text, string time, string image)
        {
            userName = name;
            postText = text;
            commentCreated = time;
            userImage = image;
        }
    }
}
