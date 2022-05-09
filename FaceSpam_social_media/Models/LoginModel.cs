using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using FaceSpam_social_media.DbModels;

namespace FaceSpam_social_media.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Enter login.")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Enter password.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public bool Verify(mydbContext context)
        {
            bool result = context.Users.Any(x => x.Name == Login && x.Password == Password);

            return result;
        }

    }

}
