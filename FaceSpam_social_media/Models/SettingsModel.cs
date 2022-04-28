using System.Linq;

namespace FaceSpam_social_media.Models
{
    public class SettingsModel
    {
        public DbModels.User user = new DbModels.User();
        public string email { get; set; }
        public string name { get; set; }
        public string description { get; set; }

        public void ChangeUserInfo(DbModels.mydbContext context, string email, string name, string description)
        {
            int id = user.UserId;
            user = context.Users.Where(x => x.UserId == id ).FirstOrDefault();
            user.Email = email;
            user.Name = name;
            user.Description = description;

            context.SaveChanges();
        }

    }
}
