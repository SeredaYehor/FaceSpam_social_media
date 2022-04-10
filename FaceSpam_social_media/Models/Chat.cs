using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FaceSpam_social_media.Models
{
    public class Chat
    {
        private int _chatId;
        private string _description;
        private string _chatName;
        private int _admin;
        private DateTime _dateCreating;
        private string _imageRef;

        public Chat(int chatId, string chatName, string description,
            int admin, DateTime dateCreating, string imageRef)
        {
            _chatId = chatId;
            _chatName = chatName;
            _description = description;
            _dateCreating = dateCreating;
            _admin = admin;
            _imageRef = imageRef;
        }

        public List<Message> chatMessages = new List<Message>();

        public int Id { get => _chatId;  }
        public string Name { get => _chatName;  }
        public string Description { get => _description; }
        public int Admin { get => _admin; }
        public DateTime DateCreating { get => _dateCreating; }
        public string ImageReference { get => _imageRef; }

    }
}
