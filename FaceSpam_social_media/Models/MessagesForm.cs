using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using FaceSpam_social_media.Infrastructure.Data;
using FaceSpam_social_media.Infrastructure.Repository;
using FaceSpam_social_media.Services;

namespace FaceSpam_social_media.Models
{
    public class MessagesForm
    {

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

        public Chat GetChatMessages(IChatMemberService chatMemberService, IMessageService service, int chatId)
        {
            currentChat = 0;
            if (chatId != 0)
            {
                currentChat = chatId;
                chatMessages = service.GetMessages(chatId);
                selectedChat = chats.Where(x => x.Id == chatId).First();
                members = chatMemberService.GetChatMembers(chatId);
                return chats.Where(x => x.Id == currentChat).First();
            }
            return new Chat();
        }
    }
}
