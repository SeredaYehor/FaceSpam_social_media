using System;
using System.Collections.Generic;
using System.Linq;
using FaceSpam_social_media.Infrastructure.Data;
using System.Threading.Tasks;
using FaceSpam_social_media.Infrastructure.Repository;
using FaceSpam_social_media.Services;

namespace FaceSpam_social_media.Models
{
    public class PostCommentsModel
    {
        public User user = new User();
        public Post post = new Post();
        public List<User> users = new List<User>();
        public List<Message> chatMessages = new List<Message>();
        public int mainUserId;
        
        public IRepository _repository;

        public PostCommentsModel()
        {
        }


        public void GetComments(int id, IMessageService service)
        {
            post = _repository.GetAll<Post>().Where(x => x.Id == id).FirstOrDefault();
            post.Messages = service.GetMessages(id, true);
            post.UserUser = _repository.GetAll<User>().Where(x=>x.Id == post.UserUserId).FirstOrDefault();

            foreach (var comment in post.Messages)
            {
                users.Add(_repository.GetAll<User>().Where(x => x.Id == comment.UserUserId).FirstOrDefault());
            }
        }

        public async Task<int> RemoveComment(int commentId, IMessageService service)
        {
            Message remove = await service.DeleteMessage(commentId);
            chatMessages.Remove(remove);
            user.Messages.Remove(remove);
            return remove.Id;
        }

        public async Task<int> AddComment(string message, IMessageService service)
        {
            var newComment = await service.AddMessage(post.Id, user.Id, message, true);

            post.Messages.Add(newComment);
            return newComment.Id;
        }
    }
}
