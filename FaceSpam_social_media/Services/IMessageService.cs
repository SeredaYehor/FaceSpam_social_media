using System.Threading.Tasks;
using System.Collections.Generic;
using FaceSpam_social_media.Infrastructure.Data;

namespace FaceSpam_social_media.Services
{
    public interface IMessageService
    {
        public List<Message> GetMessages(int id, bool comments = false);

        public Task<Message> AddMessage(int chatId, int userId, string message, bool comments = false);

        public Task<Message> DeleteMessage(int messageId);

    }
}