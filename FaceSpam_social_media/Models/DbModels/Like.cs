using System;
using System.Collections.Generic;

#nullable disable

namespace FaceSpam_social_media.Models.DbModels
{
    public partial class Like
    {
        public int LikeId { get; set; }
        public int UserUserId { get; set; }
        public int PostPostId { get; set; }
        public int MessageMessageId { get; set; }

        public virtual Message MessageMessage { get; set; }
        public virtual User UserUser { get; set; }
    }
}
