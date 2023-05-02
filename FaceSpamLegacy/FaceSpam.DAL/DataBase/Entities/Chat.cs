namespace FaceSpam.DAL.DataBase.Entities;

public class Chat : BaseEntity
{
    public string Name { get; set; }
    public string Description { get; set; }
    public Guid AdminId { get; set; }
    public DateTime DateOfCreating { get; set; }
    public string ImageReference { get; set; }

    public virtual ICollection<User> Members { get; set; }
    
    public virtual ICollection<Message> Messages { get; set; }
}