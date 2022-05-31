using System;
using System.Collections.Generic;
using System.Linq;
using FaceSpam_social_media.Infrastructure.Data;
using System.Threading.Tasks;
using FaceSpam_social_media.Infrastructure.Repository;

namespace FaceSpam_social_media.Models
{
    public class PostCommentsModel
    {
        private readonly IRepository _repository;

        public PostCommentsModel(IRepository repository)
        {
            _repository = repository;
        }
        public User user = new User();
        public Post post = new Post();
        public List<User> users = new List<User>();

        public void GetComments(MVCDBContext context)
        {
            post.Messages = context.Messages.Where(x => x.PostPostId == post.Id/*PostId*/).ToList();
            post.UserUser = context.Users.Where(x=>x.Id == post.UserUserId).FirstOrDefault();

            foreach (var comment in post.Messages)
            {
                users.Add(context.Users.Where(x => x.Id == comment.UserUserId).FirstOrDefault());
            }
        }

        public void AddComment(MVCDBContext context, string message)
        {
            Message newMessage = new Message();
            newMessage.PostPostId = post.Id/*PostId*/;
            newMessage.UserUserId = user.Id;
            newMessage.Text = message;
            newMessage.DateSending = DateTime.Now;

            context.Messages.Add(newMessage);
            context.SaveChanges();
        }
    }
}
