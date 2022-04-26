using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FaceSpam_social_media.Models
{
    public class LoginModel
    {
        public string Login { get; set; }
        public string Password { get; set; }

        public bool Verify(DbModels.mydbContext context)
        {
            bool result = context.Users.Any(x => x.Name == Login && x.Password == Password);

            return result;
        }

    }

}
