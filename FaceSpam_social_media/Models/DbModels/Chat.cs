using System;
using System.Collections.Generic;

#nullable disable

namespace FaceSpam_social_media.Models.DbModels
{
    public partial class Chat
    {
        public Chat()
        {
            ChatMembers = new HashSet<ChatMember>();
            Messages = new HashSet<Message>();
        }

        public int ChatId { get; set; }
        public string Description { get; set; }
        public string ChatName { get; set; }
        public int Admin { get; set; }
        public DateTime DateCreating { get; set; }
        public string ImageReference { get; set; }

        public virtual ICollection<ChatMember> ChatMembers { get; set; }
        public virtual ICollection<Message> Messages { get; set; }
    }
}
