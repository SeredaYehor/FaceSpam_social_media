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

        public PostCommentsModel()
        {
        }


        public void GetComments(int id, IMessageService messageService, 
             IUserService userService, Main mainFormModel)
        {
            post = mainFormModel.user.Posts.Where(x=>x.Id == id).FirstOrDefault();
            post.Messages = messageService.GetMessages(id, true);
            post.UserUser = userService.GetUser(post.UserUserId);

            foreach (var comment in post.Messages)
            {
                users.Add(userService.GetUser(comment.UserUserId));
            }
        }
    }
}
