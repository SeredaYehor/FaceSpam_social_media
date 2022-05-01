using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace FaceSpam_social_media.Models
{
    public class AuthenticationModel
    {
        [Required(ErrorMessage ="Enter login.")]
        [StringLength(45, ErrorMessage = "The field must have between {2} and {1} characters.", MinimumLength = 4)]
        public string Login { get; set; }

        [Required(ErrorMessage = "Enter password.")]
        [StringLength(20, ErrorMessage = "The field must have between {2} and {1} characters.", MinimumLength = 5)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Enter confirm password.")]
        [Compare("Password", ErrorMessage = "Passwords do not match.")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage ="Enter email.")]
        [StringLength(40, ErrorMessage = "The field must have between {2} and {1} characters.", MinimumLength = 5)]
        [DataType(DataType.EmailAddress, ErrorMessage = "E-mail is not valid.")]
        public string Email { get; set; }

        public bool Verify(DbModels.mydbContext context)
        {
            bool result = context.Users.Any(x => x.Name == Login || x.Email == Email);

            return result;
        }

        public void CreateUser(string login, string password, string email, DbModels.mydbContext context)
        {
            DbModels.User send = new DbModels.User();
            send.Name = login;
            send.Password = password;
            send.Email = email;
            send.ImageReference = "../Images/DefaultUserImage.png";
            context.Users.Add(send);
            context.SaveChanges();

        }
    }

}
