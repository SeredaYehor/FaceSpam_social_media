using System;
using System.Linq;
using System.Threading.Tasks;
using Castle.Core.Internal;
using Microsoft.EntityFrameworkCore;
using FaceSpam_social_media.Infrastructure.Data;
using FaceSpam_social_media.Infrastructure.Repository;

namespace FaceSpam_social_media.Services
{
    public class PostService : IPostService
    {
        private readonly IRepository _repository;

        public PostService(IRepository repository)
        {
            _repository = repository;
        }
        /*public async Task GetPost()
        {
        }*/

        public async Task<Post> AddPost(Post post)
        {
            var newPost = await _repository.AddAsync(post);
            return newPost;
        }

        /*public async Task UpdatePost()
        {
           
        }*/
    }
}