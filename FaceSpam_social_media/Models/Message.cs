using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FaceSpam_social_media.Models
{
    public class Message
    {
        private int _messageId;
        private string _text;
        private DateTime _time;
        private int _userId;
        private int _postId;
        private int _chatId;
        public Message(int messageId, string text, DateTime time, int userId, int postId = 0,
            int chatId = 0)
        {
            _messageId = messageId;
            _text = text;
            _time = time;
            _userId = userId;
            _postId = postId;
            _chatId = chatId;
        }

        public int ID { get => _messageId; }
        public string Text { get => _text; }
        public string Time { get => _time.ToString("HH:MM:ss"); }
        public int UserID { get => _userId; }
        public int PostId { get => _postId; }
        public int ChatId { get => _chatId; }
    }
}
