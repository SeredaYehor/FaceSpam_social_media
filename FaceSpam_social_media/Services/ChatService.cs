using System;
using System.Linq;
using System.Threading.Tasks;
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

        /*public async Task<int> AddChat()
        {
            
        }
        public async Task UpdateChat()
        {
           
        }*/
    }
}