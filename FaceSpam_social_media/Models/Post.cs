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

        public Post(int inputId, string inputText,string inputReference = null)
        {
            id = inputId;
            text = inputText;
            timePosting = DateTime.Now;
            imageReference = inputReference;
        }

        public string Text
        {
            get => text;
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
