using FaceSpam_social_media.Infrastructure.Data;
using FaceSpam_social_media.Infrastructure.Repository;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace FaceSpam_social_media.Models
{
    public class AuthenticationModel
    {
        public IRepository _repository;

        public AuthenticationModel()
        {
        }

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

        public bool Verify(string login, string email)
        {
            bool result = _repository.GetAll<User>().Any(u => u.Name == login || u.Email == email);

            return result;
        }

    }

}
