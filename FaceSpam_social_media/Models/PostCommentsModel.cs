using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FaceSpam_social_media.Models
{
    public class PostCommentsModel
    {
        public DbModels.User user = new DbModels.User();
        public DbModels.Post post = new DbModels.Post();
        public List<DbModels.User> users = new List<DbModels.User>();

        public int mainUserId;
        public void GetComments(DbModels.mydbContext context, int postId)
        {
            post = context.Posts.Where(x => x.PostId == postId).FirstOrDefault();
            post.Messages = context.Messages.Where(x => x.PostPostId == post.PostId).ToList();
            post.UserUser = context.Users.Where(x=>x.UserId == post.UserUserId).FirstOrDefault();

            foreach (var comment in post.Messages)
            {
                users.Add(context.Users.Where(x => x.UserId == comment.UserUserId).FirstOrDefault());
            }
        }

        public int RemoveComment(DbModels.mydbContext context, int commentId)
        {
            int result = -1;
            DbModels.Message remove = context.Messages.Where(x => x.MessageId == commentId).First();
            context.Messages.Remove(remove);
            result = context.SaveChanges();
            user.Messages.Remove(remove);
            return result;
        }

        public int AddComment(DbModels.mydbContext context, string message)
        {
            DbModels.Message newMessage = new DbModels.Message();
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
