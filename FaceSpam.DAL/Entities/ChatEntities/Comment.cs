using FaceSpam.DAL.Entities.UserEntities;

namespace FaceSpam.DAL.Entities.ChatEntities;

public class Comment : BaseEntity
{
    public string Text { get; set; }
    public DateTime DateSending { get; set; }
    public Guid UserId { get; set; }
    public Guid PostId { get; set; }
    public Guid? ParentId { get; set; }

    public virtual ICollection<Comment> Comments { get; set; }
    
    public virtual Comment? Parent { get; set; }
    
    public virtual Post Post { get; set; }
    
    public virtual User User { get; set; }
    
    public virtual ICollection<Like> Likes { get; set; }
}