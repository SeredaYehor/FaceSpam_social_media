namespace FaceSpam.DAL.DataBase.Entities;

public class Post : BaseEntity
{
    public string Text { get; set; }
    public DateTime PostingDate { get; set; }
    public string ImageReference { get; set; }
    public Guid UserId { get; set; }

    public virtual User User { get; set; }

    public virtual ICollection<Message> Comments { get; set; }

    public virtual ICollection<Like> Likes { get; set; }
}