using FaceSpam_social_media.Infrastructure.Data;
using FaceSpam_social_media.Infrastructure.Repository;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace FaceSpam_social_media.Models
{
    public class SettingsModel
    {
        public IRepository _repository;
        public SettingsModel()
        {

        }
    
        public User user = new User();

        [StringLength(40, ErrorMessage = "The field must have between {2} and {1} characters.", MinimumLength = 5)]
        [DataType(DataType.EmailAddress, ErrorMessage = "E-mail is not valid.")]
        public string email { get; set; }
        [StringLength(45, ErrorMessage = "The field must have between {2} and {1} characters.", MinimumLength = 4)]
        public string name { get; set; }
        [StringLength(255, ErrorMessage = "The field must have between {2} and {1} characters.", MinimumLength = 4)]
        public string description { get; set; }

    }
}
