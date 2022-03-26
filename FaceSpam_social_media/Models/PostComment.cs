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
            get => commentCreated;
        }

        public PostComment(string name, string text, DateTime time)
        {
            userName = name;
            postText = text;
            commentCreated = time;
        }
    }
}
