using System;
using System.Collections.Generic;

#nullable disable

namespace FaceSpam_social_media.Models.DbModels
{
    public partial class Message
    {
        public Message()
        {
            Likes = new HashSet<Like>();
        }

        public int MessageId { get; set; }
        public string Text { get; set; }
        public DateTime DateSending { get; set; }
        public int UserUserId { get; set; }
        public int PostPostId { get; set; }
        public int ChatChatId { get; set; }

        public virtual Chat ChatChat { get; set; }
        public virtual User UserUser { get; set; }
        public virtual ICollection<Like> Likes { get; set; }
    }
}
