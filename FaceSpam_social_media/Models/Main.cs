using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FaceSpam_social_media.Infrastructure.Data;
using FaceSpam_social_media.Infrastructure.Repository;

namespace FaceSpam_social_media
{
    public class Main
    {
        public IRepository _repository;

        public Main()
        {
        }
        public User user;
        public List<User> friends;

        // this field will be used to chech the opened user page
        // as a result view will be changed, if mainUserId is eqhal to user.UserID 
        public int mainUserId;

        public void UpdateMainUserInfo(string name, string email, string description)
        {
            user.Name = name;
            user.Email = email;
            user.Description = description;
        }
        public int AddPost(MVCDBContext context, string message, string reference)
        {
            Post newPost = new Post();
            newPost.Text = message;
            newPost.UserUserId = user.Id;
            newPost.DatePosting = DateTime.Now;
            if(reference != null)
            {
                newPost.ImageReference = reference;
            }
            context.Posts.Add(newPost);
            context.SaveChanges();
            user.Posts.Add(newPost);
            return newPost.Id/*PostId*/;
        }

        public void GetLikes(/*MVCDBContext context*/)
        {
            user.Likes = _repository.GetAll<Like>().Where(x => x.UserUserId == user.Id).ToList();
            //user.Likes = context.Likes.Where(x => x.UserUserId == user.Id).ToList();
        }

        public int CountLikes(int postId)
        {
            return user.Likes.Where(x => x.PostPostId == postId).Count();
        }

        public bool CheckLike(int userId, int postId)
        {
            return user.Likes.Any(x => x.UserUserId == userId && x.PostPostId == postId);
        }

        public void UpdatePostLike(MVCDBContext context, int postId)
        {
            if(CheckLike(user.Id, postId))
            {
                context.Likes.Remove(context.Likes
                    .Where(x => x.UserUserId == user.Id && x.PostPostId == postId).First());
                context.SaveChanges();
                user.Likes.Remove(user.Likes
                    .Where(x => x.UserUserId == user.Id && x.PostPostId == postId).First());
            }
            else
            {
                Like like = new Like() { UserUserId = user.Id, PostPostId = postId };
                context.Likes.Add(like);
                context.SaveChanges();
                user.Likes.Add(like);
            }
        }

        public void GetUser(string name, string password)
        {
            user = _repository.GetAll<User>().Where(x => x.Name == name && x.Password == password)
                .FirstOrDefault();

            mainUserId = user.Id;
            user.Password = null;
        }

        public void GetPosts(/*MVCDBContext context*/)
        {
            user.Posts = _repository.GetAll<Post>().Where(x => x.UserUser == user).ToList();
            //user.Posts = context.Posts.Where(x => x.UserUser == user).ToList();
        }

        public void GetFriends(MVCDBContext context)
        {
            friends = context.Friends.Where(x => x.UserUserId == user.Id)
                .Select(x => x.FriendNavigation).ToList();
        }

        public void GetMainUserInfo(MVCDBContext context, string name, string password)
        {
            GetUser(name, password);
            GetPosts(/*context*/);
            GetFriends(context);
            GetLikes(/*context*/);
        }

        // this function gets user info by id
        // password is cleaned
        public void GetUserInfo(MVCDBContext context, int id)
        {
            user = _repository.GetAll<User>().Where(x => x.Id == id).FirstOrDefault();
            GetPosts(/*context*/);
            GetFriends(context);
            GetLikes(/*context*/);

            user.Password = null;
        }

        public string message { get; set; }
    }
}
