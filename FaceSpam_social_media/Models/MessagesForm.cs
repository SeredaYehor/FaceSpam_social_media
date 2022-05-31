using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using FaceSpam_social_media.Infrastructure.Data;
using FaceSpam_social_media.Infrastructure.Repository;

namespace FaceSpam_social_media.Models
{
    public class MessagesForm
    {
        private readonly IRepository _repository;

        public MessagesForm(IRepository repository)
        {
            _repository = repository;
        }
        public User user = new User();
        public List<Chat> chats = new List<Chat>();
        public List<Message> chatMessages = new List<Message>();
        public string message { get; set; }
        public int currentChat { get; set; }
        public void GetChatMessages(MVCDBContext context, int chatId)
        {
            currentChat = chatId;
            chatMessages = context.Messages.Where(x => x.ChatChatId == currentChat)
                .Include(x => x.UserUser).ToList();
        }

        public void SendMessage(MVCDBContext context, string inputMessage)
        {
            Message send = new Message();
            send.Text = inputMessage;
            send.ChatChatId = currentChat;
            send.DateSending = DateTime.Now;
            send.UserUserId = user.Id;
            context.Messages.Add(send);
            context.SaveChanges();
            chatMessages.Add(send);
        }

        public void GetChats(MVCDBContext context)
        {
            chats = context.ChatMembers.Where(x => x.UserUserId == user.Id)
                .Select(x => x.ChatChat).ToList();
        }
    }
}
