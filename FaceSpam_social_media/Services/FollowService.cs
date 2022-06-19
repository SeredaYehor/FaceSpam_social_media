using System;
using System.Linq;
using System.Threading.Tasks;
using Castle.Core.Internal;
using Microsoft.EntityFrameworkCore;
using FaceSpam_social_media.Infrastructure.Data;
using FaceSpam_social_media.Infrastructure.Repository;
using System.Collections.Generic;

namespace FaceSpam_social_media.Services
{
    public class FollowService : IFollowService
    {
        private readonly IRepository _repository;

        public FollowService(IRepository repository)
        {
            _repository = repository;
        }

        public List<Friend> GetFollowings(int followerId) => _repository
            .GetAll<Friend>().Where(x=>x.UserUserId == followerId).ToList();

        public List<User> GetFollowingUsers(int followerId) => _repository
            .GetAll<Friend>().Where(x=>x.UserUserId == followerId)
            .Select(x=>x.FriendNavigation).ToList();

        public async Task<User> Addfollowing(int followerId, int followId) 
        {
            Friend newFollowing = await _repository.AddAsync(new Friend
            {
                UserUserId = followerId,
                FriendId = followId
            });

            return newFollowing.FriendNavigation; 
        }

        public async Task<Friend> RemoveFollowing(int followerId, int followingId) 
        {
            Friend deleteFollowing = _repository.GetAll<Friend>()
                .Where(x => x.FriendId == followingId && x.UserUserId == followerId).FirstOrDefault();

            await _repository.DeleteAsync(deleteFollowing);

            return deleteFollowing; 
        }

        public bool IsFollowed(int followerId, int followingId)
        {
            List<Friend> executorFollowings = _repository.GetAll<Friend>()
                .Where(x=>x.UserUserId == followerId).ToList();

            return executorFollowings.Any(x=>x.FriendId == followingId);
        }
    }
}