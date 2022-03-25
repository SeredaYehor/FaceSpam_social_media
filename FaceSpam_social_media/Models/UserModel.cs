using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FaceSpam_social_media.Models
{
    public class UserModel
    {
        public UserModel(string name, string photo = null)
        {
            userName = name;
            photoLink = photo;
        }

        private string userName;
        private string photoLink;

        public string getName
        {
            get => userName;
        }

        public string getPhoto
        {
            get => photoLink;
        }
    }
}
