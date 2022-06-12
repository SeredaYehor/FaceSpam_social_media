using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FaceSpam_social_media.Infrastructure.Data;
using FaceSpam_social_media.Infrastructure.Repository;

namespace FaceSpam_social_media.Models
{
    public class UsersManagment
    {
        public User admin;
        public List<User> users;

        public IRepository _repository;
        public UsersManagment()
        {

        }
        public bool Init(User user)
        {
            bool result = false;
            if(user.IsAdmin == true)
            {
                admin = user;
                GetAllUsers();
                result = true;
            }
            return result;
        }

        public void GetAllUsers()
        {
            users = _repository.GetAll<User>().Where(x => x.Id != admin.Id).ToList();
        }

        public async Task<int> UpdateStatus(int userId)
        {
            User target = users.Where(x => x.Id == userId).First();
            bool? status = target.IsBanned;
            target.IsBanned = !status;
            target = await _repository.UpdateAsync(target);
            return target.Id;
        }
    }
}
