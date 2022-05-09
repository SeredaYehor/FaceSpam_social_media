using System.ComponentModel.DataAnnotations;
using System.Linq;
using FaceSpam_social_media.DbModels;

namespace FaceSpam_social_media.Models
{
    public class SettingsModel
    {
        public User user = new User();

        [StringLength(40, ErrorMessage = "The field must have between {2} and {1} characters.", MinimumLength = 5)]
        [DataType(DataType.EmailAddress, ErrorMessage = "E-mail is not valid.")]
        public string email { get; set; }
        [StringLength(45, ErrorMessage = "The field must have between {2} and {1} characters.", MinimumLength = 4)]
        public string name { get; set; }
        [StringLength(255, ErrorMessage = "The field must have between {2} and {1} characters.", MinimumLength = 4)]
        public string description { get; set; }

        public void ChangeUserInfo(mydbContext context, string email, string name, string description)
        {
            int id = user.UserId;
            user = context.Users.Where(x => x.UserId == id ).FirstOrDefault();

            bool repeatEmail = context.Users.Any(x => x.Email == email);
            bool repeatName = context.Users.Any(x => x.Name == name);

            if (email != null) 
            {
                if(repeatEmail != true)
                {
                    user.Email = email;
                }
            }
            if(name != null)
            {
                if(repeatName != true)
                {
                    user.Name = name;
                }
            }
            if(description != null)
            {
                user.Description = description;
            }
            context.SaveChanges();
        }
    }
}
