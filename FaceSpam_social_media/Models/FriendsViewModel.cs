using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FaceSpam_social_media.Infrastructure.Data;
using FaceSpam_social_media.Infrastructure.Repository;

namespace FaceSpam_social_media.Models
{
    public class FriendsViewModel
    {
        public User user;
        public List<User> friends = new List<User>();
        public List<User> allUsers = new List<User>();
        public int executor;

        public bool friendPage;

        public IRepository _repository;

        public FriendsViewModel()
        {
        }

        public void GetUserById(int id)
        {
            user = _repository.GetAll<User>().Where(x => x.Id == id).FirstOrDefault();

            friends = _repository.GetAll<Friend>().Where(x => x.UserUserId == user.Id)
                .Select(x => x.FriendNavigation).ToList();
        }

        public void GetAllUsers()
        {
            allUsers = _repository.GetAll<User>().Where(x=>x.Id != executor).ToList();
        }

        public async Task DeleteFriend(int id)
        {
            var friend = new Friend();
            friend = _repository.GetAll<Friend>()
                .Where(x => x.FriendId == id && x.UserUserId == executor).FirstOrDefault();

            await _repository.DeleteAsync(friend);
            friends.Remove(friends.Where(x => x.Id == id).FirstOrDefault());
        }

        public async Task AddFriend(int id)
        {
            var newFriend = await _repository.AddAsync(new Friend
            {
                UserUserId = executor,
                FriendId = id
            });

            friends.Add(_repository.GetAll<User>().Where(x => x.Id == id).FirstOrDefault());
        }

        public string IsFriend(int userId)
        {
            string result = "Pal up";
            foreach(var user in friends)
            {
                if(user.Id == userId)
                {
                    result = "Remove";
                    break;
                }
            }         

            return result;
        }

        public void GetMainFormData(Main mainModel)
        {
            friends = mainModel.friends;
            executor = mainModel.executor.Id;
        }
    }
}
