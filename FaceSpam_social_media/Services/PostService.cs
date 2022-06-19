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
    public class PostService : IPostService
    {
        private readonly IRepository _repository;

        public PostService(IRepository repository)
        {
            _repository = repository;
        }

        public List<Post> GetPosts(User user)
            => _repository.GetAll<Post>().Where(x => x.UserUser == user)
            .OrderByDescending(d => d.DatePosting).ToList();

        public Post GetPostById(int postId)
            => _repository.GetAll<Post>().Where(x=>x.Id == postId).FirstOrDefault();

        public async Task<int> AddPost(int userId, string message, string reference)
        {
            Post newPost = new Post
            {
                Text = message,
                UserUserId = userId,
                DatePosting = DateTime.Now
            };

            if (reference != null)
            {
                newPost.ImageReference = reference;
            }
            newPost = await _repository.AddAsync(newPost);
            return newPost.Id;
        }

        public async Task RemoveChildRows(int postId, bool removeLikes)
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
            await RemoveChildRows(postId, false);
            await RemoveChildRows(postId, true);
            Post remove = _repository.GetAll<Post>().Where(x => x.Id == postId)
                .First();
            await _repository.DeleteAsync(remove);
            return remove.Id;
        }
    }
}