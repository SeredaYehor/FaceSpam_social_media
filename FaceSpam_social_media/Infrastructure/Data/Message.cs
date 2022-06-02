using System;
using System.Collections.Generic;

#nullable disable

namespace FaceSpam_social_media.Infrastructure.Data
{
    public partial class Message : IEntity
    {
        public Message()
        {
            Likes = new HashSet<Like>();
        }

        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime DateSending { get; set; }
        public int UserUserId { get; set; }
        public int? PostPostId { get; set; }
        public int? ChatChatId { get; set; }

        public virtual Chat ChatChat { get; set; }
        public virtual Post PostPost { get; set; }
        public virtual User UserUser { get; set; }
        public virtual ICollection<Like> Likes { get; set; }
    }
}
