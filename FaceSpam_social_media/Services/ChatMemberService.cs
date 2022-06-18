using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FaceSpam_social_media.Infrastructure.Data;
using FaceSpam_social_media.Infrastructure.Repository;

namespace FaceSpam_social_media.Services
{
    public class ChatMemberService : IChatMemberService
    {
        private readonly IRepository _repository;

        public ChatMemberService(IRepository repository)
        {
            _repository = repository;
        }

        public List<User> GetChatMembers(int chatId)
            => _repository.GetAll<ChatMember>().Where(x => x.ChatChatId == chatId)
            .Select(y => y.UserUser).ToList();

        public async Task<int> AddChatMember(int chatId, int memberId)
        {
            ChatMember newMember = new ChatMember
            {
                ChatChatId = chatId,
                UserUserId = memberId
            };
            await _repository.AddAsync<ChatMember>(newMember);
            return GetChatMembers(chatId).Count;
        }

        public async Task<int> RemoveChatMember(int chatId, int memberId)
        {
            ChatMember remove = _repository.GetAll<ChatMember>().Where(x => x.UserUserId == memberId
            && x.ChatChatId == chatId).First();
            await _repository.DeleteAsync(remove);
            return GetChatMembers(chatId).Count;
        }
    }
}
