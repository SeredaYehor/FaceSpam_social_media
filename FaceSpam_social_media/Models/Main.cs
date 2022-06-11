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
        public User user;
        public User executor;
        public List<User> friends;
        public string message { get; set; }

        public IRepository _repository;

        public Main()
        {
        }

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
            var newPost = await _repository.AddAsync(new Post
            {
                Text = message,
                UserUserId = user.Id,
                DatePosting = DateTime.Now
            });

            if (reference != null)
            {
                newPost.ImageReference = reference;
            }

            user.Posts.Add(newPost);
            return newPost.Id;
        }


        private async Task RemoveChildRows(int postId, bool removeLikes)
        {
            if (removeLikes)
            {
                IEnumerable<Like> likesRemove = _repository.GetAll<Like>().Where(x => x.PostPostId == postId);
                await _repository.DeleteAsyncRange(likesRemove);
            }
            else
            {
                List<Message> messagesRemove = _repository.GetAll<Message>().Where(x => x.PostPostId == postId).ToList();
                await _repository.DeleteAsyncRange(messagesRemove);
            }
        }

        public async Task<int> RemovePost(int postId)
        {
            user.Posts.Remove(user.Posts.Where(x => x.Id == postId && x.UserUserId == user.Id).First());
            await RemoveChildRows(postId, false);
            await RemoveChildRows(postId, true);
            Post remove = _repository.GetAll<Post>().Where(x => x.Id == postId && x.UserUserId == user.Id)
                .First();
            await _repository.DeleteAsync(remove);
            return remove.Id;
        }

        public void GetLikes()
        {
            user.Likes = _repository.GetAll<Like>().Where(x => x.PostPost.UserUserId == user.Id).ToList();
        }

        public int CountLikes(int postId)
        {
            return user.Likes.Where(x => x.PostPostId == postId).Count();
        }

        public bool CheckLike(int userId, int postId)
        {
            return user.Likes.Any(x => x.UserUserId == userId && x.PostPostId == postId);
        }

        public async Task UpdatePostLike(int postId)
        {
            int userId = executor.Id;
            bool liked = CheckLike(userId, postId);
            if (liked)
            {
                await _repository.DeleteAsync<Like>(_repository.GetAll<Like>()
                    .Where(x => x.UserUserId == userId && x.PostPostId == postId).First());
                user.Likes.Remove(user.Likes.Where(x => x.UserUserId == userId && x.PostPostId == postId).First());
            }
            else
            {
                Like like = await _repository.AddAsync(new Like
                {
                    UserUserId = userId,
                    PostPostId = postId
                });
                user.Likes.Add(like);
            }
        }

        public void GetUser(ref User current, int userId, string name, string password)
        {
            if (userId != -1)
            {
                current = _repository.GetAll<User>().Where(x => x.Id == userId).FirstOrDefault();
            }
            else
            {
                current = _repository.GetAll<User>().Where(x => x.Name == name && x.Password == password)
                .FirstOrDefault();
            }
            current.Password = "Nice try, stupid litle dum-dummy";
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

        public void GetUserInfo(bool friendDashboard, int userId, string name = null, string password = null)
        {
            GetUser(ref user, userId, name, password);
            if (!friendDashboard)
            {
                executor = user;
            }
            GetPosts();
            GetFriends();
            GetLikes();
        }

        public void UpdateData(User change)
        {
            executor.Name = change.Name;
            executor.Description = change.Description;
            executor.Email = change.Email;
        }

        public void GetMainUserInfo(string name, string password)
        {
            user = _repository.GetAll<User>().Where(x => x.Name == name && x.Password == password)
                .FirstOrDefault();
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
        }
        // function is used to learn is friend chosen person or not

        public bool isFriend;

        public bool IsFriend(int id)
        {
            foreach (var user in friends)
            {
                if (user.Id == id)
                {
                    return true;
                }
            }
            return false;
        }

    }
}
