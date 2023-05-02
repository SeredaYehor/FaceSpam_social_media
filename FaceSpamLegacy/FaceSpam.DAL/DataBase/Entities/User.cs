
namespace FaceSpam.DAL.DataBase.Entities;

public class User : BaseEntity
{
    public string Name { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    public string Description { get; set; }
    public string ImageReference { get; set; }

    public bool IsAdmin { get; set; }
    public bool IsBanned { get; set; }

    public virtual ICollection<Chat> Chats { get; set; }

    public virtual ICollection<Like> Likes { get; set; }

    public virtual ICollection<Message> Messages { get; set; }

    public virtual ICollection<Post> Posts { get; set; }

    public virtual ICollection<User> UserFollowings { get; set; }

    //public virtual ICollection<Following> UserFollowers { get; set; }
}