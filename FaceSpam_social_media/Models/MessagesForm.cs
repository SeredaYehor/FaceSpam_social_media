using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace FaceSpam_social_media.Models
{
    public class MessagesForm
    {
        public DbModels.User user = new DbModels.User();
        public List<DbModels.Chat> chats = new List<DbModels.Chat>();
        public List<DbModels.Message> chatMessages = new List<DbModels.Message>();
        public string message { get; set; }
        public int currentChat { get; set; }
        public void GetChatMessages(DbModels.mydbContext context, int chatId)
        {
            currentChat = chatId;
            chatMessages = context.Messages.Where(x => x.ChatChatId == currentChat)
                .Include(x => x.UserUser).ToList();
        }

        public void SendMessage(DbModels.mydbContext context, string inputMessage)
        {
            DbModels.Message send = new DbModels.Message();
            send.Text = inputMessage;
            send.ChatChatId = currentChat;
            send.DateSending = DateTime.Now;
            send.UserUserId = user.UserId;
            context.Messages.Add(send);
            context.SaveChanges();
            chatMessages.Add(send);
        }

        public void GetChats(DbModels.mydbContext context)
        {
            chats = context.ChatMembers.Where(x => x.UserUserId == user.UserId)
                .Select(x => x.ChatChat).ToList();
        }
    }
}
