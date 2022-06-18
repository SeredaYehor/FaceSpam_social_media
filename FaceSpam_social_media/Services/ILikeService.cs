using System.Threading.Tasks;
using System.Collections.Generic;
using FaceSpam_social_media.Infrastructure.Data;

namespace FaceSpam_social_media.Services
{
    public interface ILikeService
    {
        public List<Like> GetLikes(int userId);
        public bool CheckLike(int userId, int postId);
        public int CountLikes(int postId);
        public Task<int> UpdatePostLike(int userId, int postId);

    }
}