namespace FaceSpam.DAL.DataBase.Entities;

public class Message : BaseEntity
{
    public string Text { get; set; }
    public DateTime DateSending { get; set; }
    public Guid UserId { get; set; }
    public Guid ChatId { get; set; }
    public Guid? ParentId { get; set; }

    public virtual ICollection<Message> Replies { get; set; }
    
    public virtual Message? Parent { get; set; }
    
    public virtual Post? Post { get; set; }

    public virtual Chat? Chat { get; set; }

    public virtual User User { get; set; }
    
    public virtual ICollection<Like> Likes { get; set; }
}