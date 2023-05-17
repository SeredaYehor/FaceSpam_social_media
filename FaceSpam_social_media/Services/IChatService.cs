using System.Threading.Tasks;
using System.Collections.Generic;
using FaceSpam_social_media.Infrastructure.Data;

namespace FaceSpam_social_media.Services
{
    public interface IChatService
    {
        public List<Chat> GetChats(int userId);
        public Chat GetChatById(int chatId);
        public Task<Chat> CreateGroup(int userId, string name, string description, List<int> members, string reference);
        public Task QuitGroup(int chatId, int userId);
        public Task DeleteGroup(int Id);
    }
}