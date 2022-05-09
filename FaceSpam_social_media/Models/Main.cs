using System;
using System.Collections.Generic;
using System.Linq;
using FaceSpam_social_media.DbModels;

namespace FaceSpam_social_media
{
    public class Main
    {
        public User user;
        public User executor;
        public List<User> friends;
        public string message { get; set; }

        public int AddPost(mydbContext context, string message, string reference)
        {
            Post newPost = new Post();
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

        private void RemoveChildRows(mydbContext context, int postId, bool removeLikes)
        {
            if(removeLikes)
            {
                List<Like> likesRemove = context.Likes.Where(x => x.PostPostId == postId).ToList();
                context.Likes.RemoveRange(likesRemove);
            }
            else
            {
                List<Message> messagesRemove = context.Messages.Where(x => x.PostPostId == postId).ToList();
                context.Messages.RemoveRange(messagesRemove);
            }
            context.SaveChanges();
        }
        public int RemovePost(mydbContext context, int postId)
        {
            int entries = 1;
            RemoveChildRows(context, postId, false);
            RemoveChildRows(context, postId, true);
            Post remove = context.Posts.Where(x => x.PostId == postId && x.UserUserId == user.UserId)
                .First();
            context.Posts.Remove(remove);
            entries = context.SaveChanges();
            user.Posts.Remove(user.Posts.Where(x => x.PostId == postId && x.UserUserId == user.UserId).First());
            return entries;
        }

        public void GetLikes(mydbContext context)
        {
            user.Likes = context.Likes.Where(x => x.PostPost.UserUserId == user.UserId).ToList();
        }

        public int CountLikes(int postId)
        {
            return user.Likes.Where(x => x.PostPostId == postId).Count();
        }

        public bool CheckLike(int userId, int postId)
        {
            return user.Likes.Any(x => x.UserUserId == userId && x.PostPostId == postId);
        }

        public void UpdatePostLike(mydbContext context, int postId)
        {
            int userId = executor.UserId;
            bool liked = CheckLike(userId, postId);
            if (liked)
            {
                context.Likes.Remove(context.Likes
                    .Where(x => x.UserUserId == userId && x.PostPostId == postId).First());
                context.SaveChanges();
                user.Likes.Remove(user.Likes
                    .Where(x => x.UserUserId == userId && x.PostPostId == postId).First());
            }
            else
            {
                Like like = new Like() { UserUserId = userId, PostPostId = postId };
                context.Likes.Add(like);
                context.SaveChanges();
                user.Likes.Add(like);
            }
        }

        public void GetUser(mydbContext context, ref User current, int userId, string name = null, string password = null)
        {
            if (userId != -1)
            {
                current = context.Users.Where(x => x.UserId == userId).FirstOrDefault();
            }
            else
            {
                current = context.Users.Where(x => x.Name == name && x.Password == password)
                .FirstOrDefault();
            }
            current.Password = "Nice try, stupid litle dum-dummy";
        }

        public void GetPosts(mydbContext context)
        {
            user.Posts = context.Posts.Where(x => x.UserUser == user).ToList();
        }

        public void GetFriends(mydbContext context)
        {
            friends = context.Friends.Where(x => x.UserUserId == user.UserId)
                .Select(x => x.FriendNavigation).ToList();
        }

        public void GetUserInfo(mydbContext context, bool friendDashboard, int userId, string name = null, string password = null)
        {
            GetUser(context, ref user, userId, name, password);
            if (!friendDashboard)
            {
                executor = user;
            }
            GetPosts(context);
            GetFriends(context);
            GetLikes(context);
        }

        public void UpdateData(User change)
        {
            executor.Name = change.Name;
            executor.Description = change.Description;
            executor.Email = change.Email;
        }
    }
}
