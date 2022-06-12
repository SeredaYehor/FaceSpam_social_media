using System.Threading.Tasks;

namespace FaceSpam_social_media.Services
{
    public interface IUserService
    {
        public Task<int> AddUser(string login, string password, string email, string imageReference);
        public Task UpdateUser(int userId, string name, string email, string description);
    }
}