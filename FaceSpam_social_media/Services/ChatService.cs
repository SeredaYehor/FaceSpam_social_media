using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Castle.Core.Internal;
using Microsoft.EntityFrameworkCore;
using FaceSpam_social_media.Infrastructure.Data;
using FaceSpam_social_media.Infrastructure.Repository;

namespace FaceSpam_social_media.Services
{
    public class ChatService : IChatService
    {
        private readonly IRepository _repository;

        public ChatService(IRepository repository)
        {
            _repository = repository;
        }

        public List<Chat> GetChats(int userId)
            => _repository.GetAll<ChatMember>().Where(x => x.UserUserId == userId)
                .Select(x => x.ChatChat).ToList();

        public async Task<Chat> CreateGroup(int userId, string name, string description, List<int> members, string reference)
        {
            Chat created = new Chat()
            {
                ChatName = name,
                Description = description,
                DateCreating = DateTime.Now,
                Admin = userId,
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
            List<ChatMember> newMembers = new List<ChatMember>();
            foreach (int member in members)
            {
                ChatMember newMember = new ChatMember()
                {
                    ChatChatId = created.Id,
                    UserUserId = member
                };
                newMembers.Add(newMember);
            }
            await _repository.AddAsyncRange(newMembers);
            return newChat;
        }

        public async Task QuitGroup(int chatId, int userId)
        {
            int count = _repository.GetAll<ChatMember>()
                .Where(x => x.ChatChatId == chatId).ToList().Count;
            ChatMember chatMember = _repository.GetAll<ChatMember>()
               .Where(x => x.ChatChatId == chatId && x.UserUserId == userId).First();
            await _repository.DeleteAsync(chatMember);
            if (count == 1)
            {
                Chat remove = _repository.GetAll<Chat>().Where(x => x.Id == chatId).First();
                await _repository.DeleteAsync(remove);
            }
        }

        public async Task DeleteGroup(int Id)
        {
            List<Message> remove_messages = _repository.GetAll<Message>().Where(x => x.ChatChatId == Id).ToList();
            if (remove_messages.Count != 0)
            {
                await _repository.DeleteAsyncRange(remove_messages);
            }
            Chat remove = _repository.GetAll<Chat>().Where(x => x.Id == Id).FirstOrDefault();
            await _repository.DeleteAsync(remove);
        }
    }
}