﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FaceSpam_social_media.Models
{
    public class FriendsViewModel
    {

        public DbModels.User user;
        public List<DbModels.User> friends;

        public void DeleteFriend(DbModels.mydbContext context, int id)
        {
            DbModels.Friend friend = new DbModels.Friend();
            friend = context.Friends
                .Where(x=>x.FriendId == id && x.UserUserId==user.UserId).FirstOrDefault();

            context.Friends.Remove(friend);
            context.SaveChanges();
            friends.Remove(friends.Where(x=>x.UserId==id).FirstOrDefault());
        }
    }
}