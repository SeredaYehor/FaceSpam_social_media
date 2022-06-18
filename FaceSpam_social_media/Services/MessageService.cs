using System;
using System.Linq;
using System.Threading.Tasks;
using Castle.Core.Internal;
using Microsoft.EntityFrameworkCore;
using FaceSpam_social_media.Infrastructure.Data;
using FaceSpam_social_media.Infrastructure.Repository;
using System.Collections.Generic;

namespace FaceSpam_social_media.Services
{
    public class MessageService : IMessageService
    {
        private readonly IRepository _repository;

        public MessageService(IRepository repository)
        {
            _repository = repository;
        }

        public List<Message> GetMessages(int id, bool comments = false)
        {
            List<Message> result;

            if (comments)
            {
                result = _repository.GetAll<Message>()
                .Where(x => x.PostPostId == id).Include(x => x.UserUser).ToList();
            }
            else
            {
                result = _repository.GetAll<Message>()
                .Where(x => x.ChatChatId == id).Include(x => x.UserUser).ToList();
            }

            return result;
        }

        public async Task<Message> AddMessage(int chatId, int userId, string message, bool comments = false)
        {
            Message newMessage;

            if (comments)
            {
                newMessage = await _repository.AddAsync(new Message
                {
                    PostPostId = chatId,
                    UserUserId = userId,
                    Text = message,
                    DateSending = DateTime.Now
                });
            }
            else
            {
                newMessage = await _repository.AddAsync(new Message
                {
                    ChatChatId = chatId,
                    UserUserId = userId,
                    Text = message,
                    DateSending = DateTime.Now
                });
            }

            return newMessage;
        }

        public async Task<Message> DeleteMessage(int messageId)
        {
            Message removedMessage = _repository.GetAll<Message>().Where(x => x.Id == messageId).First();
            await _repository.DeleteAsync(removedMessage);

            return removedMessage;
        }
    }
}