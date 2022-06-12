using System;
using System.Linq;
using System.Threading.Tasks;
using Castle.Core.Internal;
using Microsoft.EntityFrameworkCore;
using FaceSpam_social_media.Infrastructure.Data;
using FaceSpam_social_media.Infrastructure.Repository;
using System.Collections.Generic;

namespace FaceSpam_social_media.Services
{
    public class UserService : IUserService
    {
        private readonly IRepository _repository;

        public UserService(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<int> AddUser(string login, string password, string email, string imageReference)
        {
            if (login.IsNullOrEmpty() || password.IsNullOrEmpty() || email.IsNullOrEmpty())
                throw new ArgumentNullException();

            var newUser = await _repository.AddAsync(new User
            {
                Name = login,
                Password = password,
                Email = email,
                ImageReference = imageReference
            });

            return newUser.Id;
        }
        public async Task UpdateUser(int userId, string name, string email, string description)
        {
            var user = await _repository.GetAll<User>()
                .FirstOrDefaultAsync(u => u.Id == userId);

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

            await _repository.UpdateAsync(user);
        }
    }
}