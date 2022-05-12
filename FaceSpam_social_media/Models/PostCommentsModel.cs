using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FaceSpam_social_media.DbModels;

namespace FaceSpam_social_media.Models
{
    public class PostCommentsModel
    {
        public User user = new User();
        public Post post = new Post();
        public List<User> users = new List<User>();

        public int mainUserId;
        public void GetComments(mydbContext context, int postId)
        {
            post = context.Posts.Where(x => x.PostId == postId).FirstOrDefault();
            post.Messages = context.Messages.Where(x => x.PostPostId == post.PostId).ToList();
            post.UserUser = context.Users.Where(x=>x.UserId == post.UserUserId).FirstOrDefault();

            foreach (var comment in post.Messages)
            {
                users.Add(context.Users.Where(x => x.UserId == comment.UserUserId).FirstOrDefault());
            }
        }

        public int RemoveComment(mydbContext context, int commentId)
        {
            int result = -1;
            Message remove = context.Messages.Where(x => x.MessageId == commentId).First();
            context.Messages.Remove(remove);
            result = context.SaveChanges();
            user.Messages.Remove(remove);
            return result;
        }

        public int AddComment(mydbContext context, string message)
        {
            Message newMessage = new Message();
            newMessage.PostPostId = post.PostId;
            newMessage.UserUserId = user.UserId;
            newMessage.Text = message;
            newMessage.DateSending = DateTime.Now;

            context.Messages.Add(newMessage);
            context.SaveChanges();
            return newMessage.MessageId;
        }
    }
}
