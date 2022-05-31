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
        private readonly IRepository _repository;

        public FriendsViewModel(IRepository repository)
        {
            _repository = repository;
        }

        public User user;
        public List<User> friends;
        public int mainUserId;

        public void GetUserById(MVCDBContext context, int id)
        {
            user = context.Users.Where(x => x.Id == id).FirstOrDefault();

            friends = context.Friends.Where(x => x.UserUserId == user.Id)
                .Select(x => x.FriendNavigation).ToList();
        }

        public void DeleteFriend(MVCDBContext context, int id)
        {
            Friend friend = new Friend();
            friend = context.Friends
                .Where(x=>x.FriendId == id && x.UserUserId==user.Id).FirstOrDefault();

            context.Friends.Remove(friend);
            context.SaveChanges();
            friends.Remove(friends.Where(x=>x.Id == id).FirstOrDefault());
        }
    }
}
