using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FaceSpam_social_media.Models
{
    //Model is useful to storage data from post requests eg. textbox
    public class User_auth
    {
        public string Login { get; set; }
        public string Password { get; set; }
    }
}
