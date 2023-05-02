using FaceSpam.DAL.Entities.UserEntities;

namespace FaceSpam.DAL.Entities.ChatEntities;

public class Post : BaseEntity
{
    public string Text { get; set; }
    public DateTime PostingDate { get; set; }
    public string ImageReference { get; set; }
    public Guid UserId { get; set; }

    public virtual User User { get; set; }

    public virtual ICollection<Comment> Comments { get; set; }

    public virtual ICollection<Like> Likes { get; set; }
}