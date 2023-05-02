using System.Collections;
using FaceSpam.DAL.Entities.ChatEntities;

namespace FaceSpam.DAL.Entities.UserEntities;

public class User : BaseEntity
{
    public string Name { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    public string Description { get; set; }
    public string ImageReference { get; set; }

    public bool IsAdmin { get; set; }
    public bool IsBanned { get; set; }

    public virtual ICollection<ChatMember> MemberOfChats { get; set; }
    
    public virtual ICollection<Like> Likes { get; set; }

    public virtual ICollection<Message> Messages { get; set; }

    public virtual ICollection<Comment> Comments { get; set; }

    public virtual ICollection<Post> Posts { get; set; }

    public virtual ICollection<Follower> Followings { get; set; }
}