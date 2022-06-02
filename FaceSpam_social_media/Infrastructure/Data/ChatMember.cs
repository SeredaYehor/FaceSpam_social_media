using System;
using System.Collections.Generic;

#nullable disable

namespace FaceSpam_social_media.Infrastructure.Data
{
    public partial class ChatMember : IEntity
    {
        public int Id { get; set; }
        public int UserUserId { get; set; }
        public int ChatChatId { get; set; }

        public virtual Chat ChatChat { get; set; }
        public virtual User UserUser { get; set; }
    }
}
