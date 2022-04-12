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
        private DateTime commentCreated;
        private string userImage;
<<<<<<< HEAD
        public PostComment(string name, string text, DateTime time, string image)
        {
            userName = name;
            postText = text;
            commentCreated = time;
            userImage = image;
        }
=======

>>>>>>> a0613f2ba4ac44925bec9e3efc1caacfcaa2ab7f
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
            get => commentCreated.ToString("dd.MM.y H:m");
        }

        public string getUserImage
        {
            get => userImage;
        }
<<<<<<< HEAD
=======

        public PostComment(string name, string text, DateTime time, string image)
        {
            userName = name;
            postText = text;
            commentCreated = time;
            userImage = image;
        }
>>>>>>> a0613f2ba4ac44925bec9e3efc1caacfcaa2ab7f
    }
}
