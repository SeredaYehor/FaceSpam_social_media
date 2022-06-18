using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FaceSpam_social_media.Infrastructure.Data;

namespace FaceSpam_social_media.Services
{
    public interface IChatMemberService
    {
        public List<User> GetChatMembers(int chatId);

        public Task<int> AddChatMember(int chatId, int memberId);

        public Task<int> RemoveChatMember(int chatId, int memberId);
    }
}
