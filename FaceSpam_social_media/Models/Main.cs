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

        public void UpdateUserInfo(string name, string email, string description)
        {
            if (name != null) { user.Name = name; }
            if (email != null) { user.Email = email; }
            if (description != null) { user.Description = description; }
        }
        public async Task<int> AddPost(string message, string reference)
        {
            Post newPost = new Post();
            newPost.Text = message;
            newPost.UserUserId = user.Id;
            newPost.DatePosting = DateTime.Now;
            if(reference != null)
            {
                newPost.ImageReference = reference;
            }
            newPost = await _repository.AddAsync<Post>(newPost);
            user.Posts.Add(newPost);
            return newPost.Id;
        }

        public void GetLikes()
        {
            user.Likes = _repository.GetAll<Like>().Where(x => x.UserUserId == user.Id).ToList();
        }

        public int CountLikes(int postId)
        {
            return user.Likes.Where(x => x.PostPostId == postId).Count();
        }

        public bool CheckLike(int userId, int postId)
        {
            return user.Likes.Any(x => x.UserUserId == userId && x.PostPostId == postId);
        }

        public async Task<int> UpdatePostLike(int postId)
        {
            if(CheckLike(user.Id, postId))
            {
                await _repository.DeleteAsync<Like>(_repository.GetAll<Like>()
                    .Where(x => x.UserUserId == user.Id && x.PostPostId == postId).First());
                user.Likes.Remove(user.Likes
                    .Where(x => x.UserUserId == user.Id && x.PostPostId == postId).First());
            }
            else
            {
                Like like = new Like() { UserUserId = user.Id, PostPostId = postId };
                like = await _repository.AddAsync<Like>(like);
                user.Likes.Add(like);
            }
            return CountLikes(postId);
        }

        public void GetUser(string name, string password)
        {
            user = _repository.GetAll<User>().Where(x => x.Name == name && x.Password == password)
                .FirstOrDefault();

            mainUserId = user.Id;
            user.Password = null;
        }

        public void GetPosts()
        {
            user.Posts = _repository.GetAll<Post>().Where(x => x.UserUser == user).ToList();
        }

        public void GetFriends()
        {
            friends = _repository.GetAll<Friend>().Where(x => x.UserUserId == user.Id)
                .Select(x => x.FriendNavigation).ToList();
        }

        public void GetMainUserInfo(string name, string password)
        {
            GetUser(name, password);
            GetPosts();
            GetFriends();
            GetLikes();
        }

        // this function gets user info by id
        // password is cleaned
        public void GetUserInfo(int id)
        {
            user = _repository.GetAll<User>().Where(x => x.Id == id).FirstOrDefault();
            GetPosts();
            GetFriends();
            GetLikes();

            user.Password = null;
        }

        public string message { get; set; }
    }
}
