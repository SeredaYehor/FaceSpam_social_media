using System.Threading.Tasks;
using System.Collections.Generic;
using FaceSpam_social_media.Infrastructure.Data;

namespace FaceSpam_social_media.Services
{
    public interface IUserService
    {
        public Task<int> AddUser(string login, string password, string email, string imageReference);
        public Task UpdateUser(int userId, string name, string email, string description, string imageReference);
        public bool repeatCheck(string input);
        public List<User> SelectAllUsers(int exceptId);
        public User GetUser(int userId, string name, string password);
        public Task<int> UpdateStatus(int userId);
        public List<User> GetAllUsers(int exceptId);
        public bool Verify(string login, string email);
    }
}