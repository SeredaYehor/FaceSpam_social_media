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

        public IRepository _repository;

        public MessagesForm()
        {
        }

        public User user = new User();
        public List<Chat> chats = new List<Chat>();
        public List<Message> chatMessages = new List<Message>();
        public string message { get; set; }
        public int currentChat { get; set; }

        public void GetChatMessages(int chatId)
        {
            currentChat = chatId;
            chatMessages = _repository.GetAll<Message>().Where(x => x.ChatChatId == currentChat)
                .Include(x => x.UserUser).ToList();
        }

        public async Task<int> RemoveMessage(int messageId)
        {
            Message remove = _repository.GetAll<Message>().Where(x => x.Id == messageId && x.UserUserId == user.Id)
                .First();
            await _repository.DeleteAsync(remove);
            chatMessages.Remove(chatMessages.Where(x => x.Id == messageId && x.UserUserId == user.Id).First());
            return remove.Id;
        }

        public async Task<int> SendMessage(string inputMessage)
        {
            var newMessage = await _repository.AddAsync(new Message
            {
                Text = inputMessage,
                ChatChatId = currentChat,
                DateSending = DateTime.Now,
                UserUserId = user.Id
            });
            chatMessages.Add(newMessage);
            return newMessage.Id;
        }
        public void GetChats()
        {
            chats = _repository.GetAll<ChatMember>().Where(x => x.UserUserId == user.Id)
                .Select(x => x.ChatChat).ToList();
        }
    }
}
