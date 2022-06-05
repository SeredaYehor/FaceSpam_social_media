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
        public IRepository _repository;

        public FriendsViewModel()
        {
        }

        public User user;
        public List<User> friends;
        public int mainUserId;

        public void GetUserById(int id)
        {
            user = _repository.GetAll<User>().Where(x => x.Id == id).FirstOrDefault();

            friends = _repository.GetAll<Friend>().Where(x => x.UserUserId == user.Id)
                .Select(x => x.FriendNavigation).ToList();
        }

        public async Task<int> DeleteFriend(int id)
        {
            var friend = new Friend();
            friend = _repository.GetAll<Friend>()
                .Where(x=>x.FriendId == id && x.UserUserId==user.Id).FirstOrDefault();

            await _repository.DeleteAsync(friend);
            friends.Remove(friends.Where(x=>x.Id == id).FirstOrDefault());
            return friend.Id;
        }
    }
}
