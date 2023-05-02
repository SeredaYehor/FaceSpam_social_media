using System.Collections;
using FaceSpam.DAL.Entities.UserEntities;

namespace FaceSpam.DAL.Entities.ChatEntities;

public class Chat : BaseEntity
{
    public string Name { get; set; }
    public string Description { get; set; }
    public Guid AdminId { get; set; }
    public DateTime DateOfCreating { get; set; }
    public string ImageReference { get; set; }

    public virtual User Admin { get; set; }

    public virtual ICollection<ChatMember> Members { get; set; }
    
    public virtual ICollection<Message> Messages { get; set; }
}