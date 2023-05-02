using FaceSpam.DAL.Entities.ChatEntities;

namespace FaceSpam.DAL.Entities.UserEntities;

public class ChatMember : BaseEntity
{
    public Guid UserId { get; set; }
    public Guid ChatId { get; set; }

    public virtual User User { get; set; }
    
    public virtual Chat Chat { get; set; }
}