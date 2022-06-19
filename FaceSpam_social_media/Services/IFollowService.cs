using System.Threading.Tasks;
using System.Collections.Generic;
using FaceSpam_social_media.Infrastructure.Data;

namespace FaceSpam_social_media.Services
{
    public interface IFollowService
    {
        public List<Friend> GetFollowings(int followerId);

        public List<User> GetFollowingUsers(int followerId);

        public Task<User> Addfollowing(int followerId, int followingId);

        public Task<Friend> RemoveFollowing(int followerId, int followingId);

        public bool IsFollowed(int followerId, int followingId);
    }
}