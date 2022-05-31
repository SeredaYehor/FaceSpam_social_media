using System.Reflection;
using Microsoft.EntityFrameworkCore;
using FaceSpam_social_media.Infrastructure.Configuration;

namespace FaceSpam_social_media.Infrastructure.Data
{

    public class MVCDBContext : DbContext
    {
        public MVCDBContext()
        {
        }
        public MVCDBContext(DbContextOptions options) : base(options)
        { }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySql("server=127.0.0.1;user=root;password=password;database=mydb", ServerVersion.Parse("8.0.25-mysql"));
            }
        }

        public virtual DbSet<Chat> Chats { get; set; }
        public virtual DbSet<ChatMember> ChatMembers { get; set; }
        public virtual DbSet<Friend> Friends { get; set; }
        public virtual DbSet<Like> Likes { get; set; }
        public virtual DbSet<Message> Messages { get; set; }
        public virtual DbSet<Post> Posts { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UsersConfiguration());
            modelBuilder.ApplyConfiguration(new ChatConfiguration());
            modelBuilder.ApplyConfiguration(new ChatMemberConfiguration());
            modelBuilder.ApplyConfiguration(new FriendConfiguration());
            modelBuilder.ApplyConfiguration(new LikeConfiguration());
            modelBuilder.ApplyConfiguration(new MessageConfiguration());
            modelBuilder.ApplyConfiguration(new PostConfiguration());
            modelBuilder.HasCharSet("utf8")
                .UseCollation("utf8_general_ci");

            base.OnModelCreating(modelBuilder);
        }
    }
}