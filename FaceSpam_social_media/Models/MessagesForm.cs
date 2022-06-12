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
        public List<User> members = new List<User>();
        public List<Message> chatMessages = new List<Message>();
        public Chat selectedChat = new Chat();
        
        public string message { get; set; }
        public int currentChat { get; set; }

        public void GetChatMessages(int chatId)
        {
            if (chatId != 0)
            {
                currentChat = chatId;
                chatMessages = _repository.GetAll<Message>().Where(x => x.ChatChatId == currentChat)
                    .Include(x => x.UserUser).ToList();
                selectedChat = chats.Where(x => x.ChatId == chatId).First();
                members = context.ChatMembers.Where(x => x.ChatChatId == currentChat).Select(y => y.UserUser).ToList();
                return chats.Where(x => x.ChatId == currentChat).First();
            }
            return new Chat();
        }

        public async Task<int> RemoveMessage(int messageId)
        {
            Message remove = _repository.GetAll<Message>().Where(x => x.Id == messageId)
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
      
         public async void QuitGroup()
        {
            ChatMember chatMember = _repository.GetAll<ChatMember>()
                .Where(x => x.ChatChatId == selectedChat.ChatId && x.UserUserId == user.Id).First();
            await _repository.DeleteAsync(chatMember);
        }

        public async void DeleteGroup(int Id)
        {
            List<Message> remove_messages = _repository.GetAll<Message>().Where(x => x.ChatChatId == Id).ToList();
            await _repository.DeleteAsyncRange(remove_messages);
            Chat remove =  _repository.GetAll<Chat>().Where(x => x.ChatId == Id).FirstOrDefault();
            await _repository.DeleteAsync(remove);
            chats.Remove(remove);
        }

        public void SelectChat(int chatId)
        {
            selectedChat = chats.Where(x => x.Id == chatId).First();
            GetChatMessages(context, chatId);
        }

        public List<User> SelectAllUsers(mydbContext context)
        {
            List<User> users =  _repository.GetAll<User>().Where(x => x.Id != user.Id).ToList();
            return users;
        }

        public async Task<Chat> CreateGroup(string name, string description, List<int> members, string reference)
        {
            Chat created = new Chat()
            {
                ChatName = name,
                Description = description,
                DateCreating = DateTime.Now,
                Admin = user.Id,
            };
            if (reference != null)
            {
                created.ImageReference = reference;
            }
            else
            {
                created.ImageReference = "../Images/default_group.jpg";
            }
            var newChat = await _repository.AddAsync(created);
            chats.Add(newChat);
            foreach (int member in members)
            {
                ChatMember newMember = new ChatMember() 
                { 
                    ChatChatId = created.ChatId, 
                    UserUserId = member 
                };
                await _repository.AddAsync(newMember);
            }
            return newChat;
        }
    }
}
