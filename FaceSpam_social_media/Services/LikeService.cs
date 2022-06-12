using System;
using System.Linq;
using System.Threading.Tasks;
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
        /*public void GetLikes()
        {
            var Likes = _repository.GetAll<Like>().Where(x => x.UserUserId == user.Id).ToList();
            //user.Likes = context.Likes.Where(x => x.UserUserId == user.Id).ToList();
        }
        */
        /*public async Task<int> AddLike()
        {
            
        }
        public async Task UpdateLike()
        {
           
        }*/
    }
}