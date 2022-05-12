using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FaceSpam_social_media.DbModels;

namespace FaceSpam_social_media.Models
{
    public class FriendsViewModel
    {

        public User user;
        public List<User> friends;
        public int mainUserId;

        public void GetUserById(mydbContext context, int id)
        {
            user = context.Users.Where(x => x.UserId == id).FirstOrDefault();

            friends = context.Friends.Where(x => x.UserUserId == user.UserId)
                .Select(x => x.FriendNavigation).ToList();
        }

        public void DeleteFriend(mydbContext context, int id)
        {
            Friend friend = new Friend();
            friend = context.Friends
                .Where(x=>x.FriendId == id && x.UserUserId==user.UserId).FirstOrDefault();

            context.Friends.Remove(friend);
            context.SaveChanges();
            friends.Remove(friends.Where(x=>x.UserId==id).FirstOrDefault());
        }
    }
}
