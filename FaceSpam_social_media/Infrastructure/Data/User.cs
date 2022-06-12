using System.Collections.Generic;

#nullable disable

namespace FaceSpam_social_media.Infrastructure.Data
{
    public partial class User : IEntity
    {
        public User()
        {
            ChatMembers = new HashSet<ChatMember>();
            FriendFriendNavigations = new HashSet<Friend>();
            FriendUserUsers = new HashSet<Friend>();
            Likes = new HashSet<Like>();
            Messages = new HashSet<Message>();
            Posts = new HashSet<Post>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public bool? IsAdmin { get; set; }
        public bool? IsBanned { get; set; }
        public string Description { get; set; }
        public string ImageReference { get; set; }

        public virtual ICollection<ChatMember> ChatMembers { get; set; }
        public virtual ICollection<Friend> FriendFriendNavigations { get; set; }
        public virtual ICollection<Friend> FriendUserUsers { get; set; }
        public virtual ICollection<Like> Likes { get; set; }
        public virtual ICollection<Message> Messages { get; set; }
        public virtual ICollection<Post> Posts { get; set; }
    }
}
