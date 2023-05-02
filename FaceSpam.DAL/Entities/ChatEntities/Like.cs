using FaceSpam.DAL.Entities.UserEntities;

namespace FaceSpam.DAL.Entities.ChatEntities;

public class Like : BaseEntity
{
    public Guid UserId { get; set; }
    public Guid? PostId { get; set; }
    public Guid? MessageId { get; set; }

    public virtual User User { get; set; }
    
    public virtual Post? Post { get; set; }
    
    public virtual Message? Message { get; set; }
}