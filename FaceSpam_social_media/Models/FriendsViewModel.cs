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
        public List<DbModels.User> allUsers = new List<DbModels.User>();
        public int mainUserId;
      
        public bool friendPage;

        public void GetUserById(mydbContext context, int id)
        {
            user = context.Users.Where(x => x.UserId == id).FirstOrDefault();

            friends = context.Friends.Where(x => x.UserUserId == user.UserId)
                .Select(x => x.FriendNavigation).ToList();
        }

        public void GetAllUsers(mydbContext context)
        {
            allUsers = context.Users.ToList();
            allUsers.Remove(allUsers.Where(x=>x.UserId == mainUserId).First());
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

        public void AddFriend(DbModels.mydbContext context, int id)
        {
            DbModels.Friend newFriend = new DbModels.Friend();
            newFriend.UserUserId = mainUserId;
            newFriend.FriendId = id;

            context.Friends.Add(newFriend);
            context.SaveChanges();

            friends.Add(context.Users.Where(x => x.UserId == id).FirstOrDefault());

        }

        public string IsFriend(int userId)
        {
            string result = "Pal up";
            foreach(var user in friends)
            {
                if(user.UserId == userId)
                {
                    result = "Remove";
                    break;
                }
            }         

            return result;
        }
    }
}
