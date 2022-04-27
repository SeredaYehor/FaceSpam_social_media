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
        public void AddPost(DbModels.mydbContext context, string message, string reference)
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

        public string message { get; set; }
    }
}
