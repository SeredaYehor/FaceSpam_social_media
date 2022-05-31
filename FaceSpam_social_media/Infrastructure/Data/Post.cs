using System;
using System.Collections.Generic;

#nullable disable

namespace FaceSpam_social_media.Infrastructure.Data
{
    public partial class Post
    {
        public Post()
        {
            Likes = new HashSet<Like>();
            Messages = new HashSet<Message>();
        }

        public int Id/*PostId*/ { get; set; }
        public string Text { get; set; }
        public DateTime DatePosting { get; set; }
        public string ImageReference { get; set; }
        public int UserUserId { get; set; }

        public virtual User UserUser { get; set; }
        public virtual ICollection<Like> Likes { get; set; }
        public virtual ICollection<Message> Messages { get; set; }
    }
}
