using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Castle.Core.Internal;
using Microsoft.EntityFrameworkCore;
using FaceSpam_social_media.Infrastructure.Data;
using FaceSpam_social_media.Infrastructure.Repository;

namespace FaceSpam_social_media.Services
{
    public class LikeService : ILikeService
    {
        private readonly IRepository _repository;

        public LikeService(IRepository repository)
        {
            _repository = repository;
        }

        public List<Like> GetLikes(int userId)
        => _repository.GetAll<Like>().Where(x => x.PostPost.UserUserId == userId).ToList();

        public bool CheckLike(int userId, int postId)
            => _repository.GetAll<Like>().Any(x => x.UserUserId == userId && x.PostPostId == postId);

        public int CountLikes(int postId)
            => _repository.GetAll<Like>().Where(x => x.PostPost.Id == postId).Count();

        public async Task<int> UpdatePostLike(int userId, int postId)
        {
            Like update = new Like();
            bool liked = CheckLike(userId, postId);
            if (liked)
            {
                await _repository.DeleteAsync<Like>(_repository.GetAll<Like>()
                    .Where(x => x.UserUserId == userId && x.PostPostId == postId).First());
            }
            else
            {
                update.UserUserId = userId;
                update.PostPostId = postId;
                update = await _repository.AddAsync(update);
            }
            int result = CountLikes(postId);
            return result;
        }
    }
}