namespace FaceSpam.DAL.Entities.UserEntities;

public class Follower : BaseEntity
{
    public Guid UserId { get; set; }
    public Guid FollowingId { get; set; }

    public virtual User User { get; set; }
    
    public virtual User Following { get; set; }
}