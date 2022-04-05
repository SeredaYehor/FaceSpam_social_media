using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FaceSpam_social_media.Models
{
    public class User
    {

        public User(int inputId, string inputName, string inputPassword, string inputEmail, 
            string inputDescription, string inputReference, bool admin = false)
        {
            userId = inputId;
            name = inputName;
            password = inputPassword;
            email = inputEmail;
            isAdmin = admin;
            description = inputDescription;
            imageReference = inputReference;
        }

        private int userId;
        public int UserID
        {
            get => userId;
            set => userId = value;
        }

        private string name;
        public string Name
        {
            get => name;
            set => name = value;
        }

        private string password;
        public string Password
        {
            get => password;
            set => password = value;
        }

        private string email;
        public string Email
        {
            get => email;
            set => email = value;
        }

        private bool isAdmin;
        public bool IsAdmin
        {
            get => isAdmin;
            set => isAdmin = value;
        }

        private string description;
        public string Description
        {
            get => description;
            set => description = value;
        }

        private string imageReference; 
    }
}
