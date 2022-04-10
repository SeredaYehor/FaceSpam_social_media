using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FaceSpam_social_media.Models
{
    public class Post
    {
        private int id;
        private string text;
        private DateTime timePosting;
        private string imageReference;
        private List<int> userLikes = new List<int>();

        public Post(int inputId, string inputText,string inputReference = null)
        {
            id = inputId;
            text = inputText;
            timePosting = DateTime.Now;
            imageReference = inputReference;
        }

        public void UpdateLike(int userId)
        {
            if(CheckLike(userId))
            {
                userLikes.Remove(userId);
            }
            else
            {
                userLikes.Add(userId);
            }
        }

        public int CountLikes()
        {
            return userLikes.Count;
        }

        public bool CheckLike(int userId)
        {
            if(userLikes.Contains(userId))
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        public string Text
        {
            get => text;
        }

        public int ID
        {
            get => id;
        }

        public string TimePosting
        {
            get => timePosting.ToString("dd.MM.yy");
        }

        public string Image
        {
            get => imageReference;
        }
    }
}
