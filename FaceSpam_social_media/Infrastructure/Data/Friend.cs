using System;
using System.Collections.Generic;

#nullable disable

namespace FaceSpam_social_media.Infrastructure.Data
{ 
    public partial class Friend : IEntity
    {
        public int Id { get; set; }
        public int UserUserId { get; set; }
        public int FriendId { get; set; }

        public virtual User FriendNavigation { get; set; }
        public virtual User UserUser { get; set; }
    }
}
