using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FaceSpam_social_media.Infrastructure.Data;
using FaceSpam_social_media.Services;
using FaceSpam_social_media.Infrastructure.Repository;

namespace FaceSpam_social_media
{
    public class Main
    {
        public User user;
        public User executor;
        public List<User> follows;
        public string message { get; set; }
        public bool isFollowed;

        public Main()
        {
        }

        // this field will be used to chech the opened user page
        // as a result view will be changed, if mainUserId is eqhal to user.UserID 
        public int mainUserId;

        public void UpdateUserInfo(string name, string email, string description, string imageReference)
        {
            if (name != null) { user.Name = name; }
            if (email != null) { user.Email = email; }
            if (description != null) { user.Description = description; }
            if (imageReference != null) { user.ImageReference = imageReference; }
        }

        public int CountLikes(int postId)
        {
            return user.Likes.Where(x => x.PostPostId == postId).Count();
        }

        public bool CheckLike(int userId, int postId)
        {
            return user.Likes.Any(x => x.UserUserId == userId && x.PostPostId == postId);
        }

        public void GetUserInfo(IUserService userService, IPostService postService, ILikeService likeService, 
            IFollowService followService, bool friendDashboard, int userId, string name = null, string password = null)
        {
            user = userService.GetUser(userId, name, password);
            if (!friendDashboard)
            {
                executor = user;
            }
            user.Posts = postService.GetPosts(user);
            follows = followService.GetFollowingUsers(user.Id);
            user.Likes = likeService.GetLikes(userId);
            isFollowed = followService.IsFollowed(executor.Id, user.Id);
        }

        public void UpdateData(User change)
        {
            executor.Name = change.Name;
            executor.Description = change.Description;
            executor.Email = change.Email;
        }

    }
}
