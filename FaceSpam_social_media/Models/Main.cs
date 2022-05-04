using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FaceSpam_social_media
{
    public class Main
    {
        public DbModels.User user;
        public List<DbModels.User> friends;

        // this field will be used to chech the opened user page
        // as a result view will be changed, if mainUserId is eqhal to user.UserID 
        public int mainUserId;
        public int AddPost(DbModels.mydbContext context, string message, string reference)
        {
            DbModels.Post newPost = new DbModels.Post();
            newPost.Text = message;
            newPost.UserUserId = user.UserId;
            newPost.DatePosting = DateTime.Now;
            if(reference != null)
            {
                newPost.ImageReference = reference;
            }
            context.Posts.Add(newPost);
            context.SaveChanges();
            user.Posts.Add(newPost);
            return newPost.PostId;
        }

        public int RemovePost(DbModels.mydbContext context, int postId)
        {
            int entries = 1;
            DbModels.Post remove = context.Posts.Where(x => x.PostId == postId && x.UserUserId == user.UserId)
                .First();
            context.Posts.Remove(remove);
            entries = context.SaveChanges();
            user.Posts.Remove(user.Posts.Where(x => x.PostId == postId && x.UserUserId == user.UserId).First());
            return entries;
        }

        public void GetLikes(DbModels.mydbContext context)
        {
            user.Likes = context.Likes.Where(x => x.UserUserId == user.UserId).ToList();
        }

        public int CountLikes(int postId)
        {
            return user.Likes.Where(x => x.PostPostId == postId).Count();
        }

        public bool CheckLike(int userId, int postId)
        {
            return user.Likes.Any(x => x.UserUserId == userId && x.PostPostId == postId);
        }

        public void UpdatePostLike(DbModels.mydbContext context, int postId)
        {
            if(CheckLike(user.UserId, postId))
            {
                context.Likes.Remove(context.Likes
                    .Where(x => x.UserUserId == user.UserId && x.PostPostId == postId).First());
                context.SaveChanges();
                user.Likes.Remove(user.Likes
                    .Where(x => x.UserUserId == user.UserId && x.PostPostId == postId).First());
            }
            else
            {
                DbModels.Like like = new DbModels.Like() { UserUserId = user.UserId, PostPostId = postId };
                context.Likes.Add(like);
                context.SaveChanges();
                user.Likes.Add(like);
            }
        }

        public void GetUser(DbModels.mydbContext context, string name, string password)
        {
            user = context.Users.Where(x => x.Name == name && x.Password == password)
                .FirstOrDefault();

            mainUserId = user.UserId;
            user.Password = null;
        }

        public void GetPosts(DbModels.mydbContext context)
        {
            user.Posts = context.Posts.Where(x => x.UserUser == user).ToList();
        }

        public void GetFriends(DbModels.mydbContext context)
        {
            friends = context.Friends.Where(x => x.UserUserId == user.UserId)
                .Select(x => x.FriendNavigation).ToList();
        }

        public void GetMainUserInfo(DbModels.mydbContext context, string name, string password)
        {
            GetUser(context, name, password);
            GetPosts(context);
            GetFriends(context);
            GetLikes(context);
        }

        // this function gets user info by id
        // password is cleaned
        public void GetUserInfo(DbModels.mydbContext context, int id)
        {
            user = context.Users.Where(x => x.UserId == id).FirstOrDefault();
            GetPosts(context);
            GetFriends(context);
            GetLikes(context);

            user.Password = null;
        }

        public string message { get; set; }
    }
}
