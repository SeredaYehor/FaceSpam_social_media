using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using FaceSpam_social_media.DbModels;

namespace FaceSpam_social_media.Models
{
    public class MessagesForm
    {
        public User user = new User();
        public List<Chat> chats = new List<Chat>();
        public List<User> members = new List<User>();
        public List<Message> chatMessages = new List<Message>();
        public Chat selectedChat = new Chat();

        public string message { get; set; }
        public int currentChat { get; set; }
        public Chat GetChatMessages(mydbContext context, int chatId)
        {
            currentChat = chatId;
            chatMessages = context.Messages.Where(x => x.ChatChatId == currentChat)
                .Include(x => x.UserUser).ToList();
            members = context.ChatMembers.Where(x => x.ChatChatId == currentChat).Select(y => y.UserUser).ToList();
            return chats.Where(x => x.ChatId == currentChat).First();
        }

        public int RemoveMessage(mydbContext context, int messageId)
        {
            int entries = 1;
            Message remove = context.Messages.Where(x => x.MessageId == messageId).First();
            context.Messages.Remove(remove);
            entries = context.SaveChanges();
            chatMessages.Remove(chatMessages.Where(x => x.MessageId == messageId).First());
            return entries;
        }

        public int SendMessage(mydbContext context, string inputMessage)
        {
            Message send = new Message();
            send.Text = inputMessage;
            send.ChatChatId = currentChat;
            send.DateSending = DateTime.Now;
            send.UserUserId = user.UserId;
            context.Messages.Add(send);
            context.SaveChanges();
            chatMessages.Add(send);
            return send.MessageId;
        }

        public void GetChats(mydbContext context)
        {
            chats = context.ChatMembers.Where(x => x.UserUserId == user.UserId)
                .Select(x => x.ChatChat).ToList();
        }

        public void SelectChat(mydbContext context, int chatId)
        {
            selectedChat = chats.Where(x => x.ChatId == chatId).First();
            GetChatMessages(context, chatId);
        }
    }
}
