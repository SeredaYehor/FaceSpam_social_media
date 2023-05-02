namespace FaceSpam.DAL.DataBase.Entities;

public class Following : BaseEntity
{
    public Guid FollowerId { get; set; }
    public Guid FollowingId { get; set; }

    public virtual User Follower { get; set; }
    public virtual User Followed { get; set; }
}