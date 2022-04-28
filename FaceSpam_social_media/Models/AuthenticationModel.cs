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
        [Required]
        public string Login { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(100, ErrorMessage = "Поле {0} должно иметь минимум {2} и максимум {1} символов.", MinimumLength = 5)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [Required]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        [DataType(DataType.Password)]
        [Display(Name = "Подтвердить пароль")]
        public string ConfirmPassword { get; set; }
        [Required]
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
            context.Users.Add(send);
            context.SaveChanges();


        }
    }

}
