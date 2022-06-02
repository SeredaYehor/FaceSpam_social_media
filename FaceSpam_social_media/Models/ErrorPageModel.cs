using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace FaceSpam_social_media.Models
{
    public class ErrorPageModel
    {
        public string WhereError { get; set; }

        public void IfError(string Error)
        {
            if (Error == "Login")
            {
                WhereError = "Wrong login or password";
            }
            if (Error == "Auth")
            {
                WhereError = "This data is already in use";
            }
            if (Error == "Ban")
            {
                WhereError = "Oi, you have been banned";
            }
        }
    }
}
