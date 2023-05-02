using FaceSpam.DAL.Entities.ChatEntities;
using FaceSpam.DAL.Entities.UserEntities;
using FaceSpam.DAL.EntityConfigs;
using Microsoft.EntityFrameworkCore;

namespace FaceSpam.DAL.Context;

public class DataBaseContext : DbContext
{
    public DataBaseContext(DbContextOptions options) : base(options) { }
    
    public virtual DbSet<Chat> Chats { get; set; }
    public virtual DbSet<ChatMember> ChatMembers { get; set; }
    public virtual DbSet<Follower> Followers { get; set; }
    public virtual DbSet<Like> Likes { get; set; }
    public virtual DbSet<Message> Messages { get; set; }
    public virtual DbSet<Comment> Comments { get; set; }
    public virtual DbSet<Post> Posts { get; set; }
    public virtual DbSet<User> Users { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new ChatConfiguration());
        modelBuilder.ApplyConfiguration(new ChatMemberConfiguration());
        modelBuilder.ApplyConfiguration(new CommentConfiguration());
        modelBuilder.ApplyConfiguration(new FollowerConfiguration());
        modelBuilder.ApplyConfiguration(new LikeConfiguration());
        modelBuilder.ApplyConfiguration(new LikeConfiguration());
        modelBuilder.ApplyConfiguration(new MessageConfiguration());
        modelBuilder.ApplyConfiguration(new PostConfiguration());
        modelBuilder.ApplyConfiguration(new UserConfiguration());

        base.OnModelCreating(modelBuilder);
    }
}