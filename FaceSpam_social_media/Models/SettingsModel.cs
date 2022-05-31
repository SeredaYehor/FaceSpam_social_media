using FaceSpam_social_media.Infrastructure.Data;
using FaceSpam_social_media.Infrastructure.Repository;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace FaceSpam_social_media.Models
{
    public class SettingsModel
    {
        /*private readonly IRepository _repository;

        public SettingsModel(IRepository repository)
        {
            _repository = repository;
        }*/

    
        public User user = new User();

        [StringLength(40, ErrorMessage = "The field must have between {2} and {1} characters.", MinimumLength = 5)]
        [DataType(DataType.EmailAddress, ErrorMessage = "E-mail is not valid.")]
        public string email { get; set; }
        [StringLength(45, ErrorMessage = "The field must have between {2} and {1} characters.", MinimumLength = 4)]
        public string name { get; set; }
        [StringLength(255, ErrorMessage = "The field must have between {2} and {1} characters.", MinimumLength = 4)]
        public string description { get; set; }

        /*public void ChangeUserInfo(string email, string name, string description)
        {
            int id = user.Id;
            
            user = _repository.GetAll<User>().Where(x => x.Id == id ).FirstOrDefault();

            bool repeatEmail = _repository.GetAll<User>().Any(x => x.Email == email);
            bool repeatName = _repository.GetAll<User>().Any(x => x.Name == name);

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
        }*/
    }
}
