using System.Threading.Tasks;
using System.Collections.Generic;
using FaceSpam_social_media.Infrastructure.Data;

namespace FaceSpam_social_media.Services
{
    public interface IPostService
    {
        public List<Post> GetPosts(User user);
        public Post GetPostById(int postId);
        public Task<int> AddPost(int userId, string message, string reference);
        public Task RemoveChildRows(int postId, bool removeLikes);
        public Task<int> RemovePost(int userId, int postId);
    }
}