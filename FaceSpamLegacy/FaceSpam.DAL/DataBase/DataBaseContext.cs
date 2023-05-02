using FaceSpam.DAL.DataBase.Entities;
using FaceSpam.DAL.DataBase.EntityConfigurations;
using Microsoft.EntityFrameworkCore;

namespace FaceSpam.DAL.DataBase;

public class DataBaseContext : DbContext
{
    public DataBaseContext()
    {
    }

    public DataBaseContext(DbContextOptions options) : base(options)
    {
    }

    public virtual DbSet<User> Users { get; set; }
    public virtual DbSet<Chat> Chats { get; set; }
    public virtual DbSet<Post> Posts { get; set; }
    public virtual DbSet<Like> Likes { get; set; }
    public virtual DbSet<Message> Messages { get; set; }
    //public virtual DbSet<Following> Followings { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfiguration(new UserConfiguration());
        builder.ApplyConfiguration(new ChatConfiguration());
        //builder.ApplyConfiguration(new FollowingConfiguration());
        builder.ApplyConfiguration(new LikeConfiguration());
        builder.ApplyConfiguration(new MessageConfiguration());
        builder.ApplyConfiguration(new PostConfiguration());
        
        base.OnModelCreating(builder);
    }
}