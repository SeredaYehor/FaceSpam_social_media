using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace FaceSpam_social_media.DbModels
{
    public partial class mydbContext : DbContext
    {
        public mydbContext()
        {
        }

        public mydbContext(DbContextOptions<mydbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Chat> Chats { get; set; }
        public virtual DbSet<ChatMember> ChatMembers { get; set; }
        public virtual DbSet<Friend> Friends { get; set; }
        public virtual DbSet<Like> Likes { get; set; }
        public virtual DbSet<Message> Messages { get; set; }
        public virtual DbSet<Post> Posts { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySql("server=127.0.0.1;user=root;password=Password;database=mydb", ServerVersion.Parse("8.0.25-mysql"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasCharSet("utf8")
                .UseCollation("utf8_general_ci");

            modelBuilder.Entity<Chat>(entity =>
            {
                entity.ToTable("chat");

                entity.Property(e => e.ChatId).HasColumnName("chat_id");

                entity.Property(e => e.ChatName)
                    .IsRequired()
                    .HasMaxLength(45)
                    .HasColumnName("Chat_name");

                entity.Property(e => e.DateCreating)
                    .HasColumnType("datetime")
                    .HasColumnName("Date_creating");

                entity.Property(e => e.Description).HasMaxLength(255);

                entity.Property(e => e.ImageReference)
                    .HasMaxLength(100)
                    .HasColumnName("Image_reference");
            });

            modelBuilder.Entity<ChatMember>(entity =>
            {
                entity.HasKey(e => new { e.UserUserId, e.ChatChatId })
                    .HasName("PRIMARY")
                    .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

                entity.ToTable("chat_members");

                entity.HasIndex(e => e.ChatChatId, "fk_User_has_Chat_Chat1_idx");

                entity.HasIndex(e => e.UserUserId, "fk_User_has_Chat_User1_idx");

                entity.Property(e => e.UserUserId).HasColumnName("User_user_id");

                entity.Property(e => e.ChatChatId).HasColumnName("Chat_chat_id");

                entity.HasOne(d => d.ChatChat)
                    .WithMany(p => p.ChatMembers)
                    .HasForeignKey(d => d.ChatChatId)
                    .HasConstraintName("fk_User_has_Chat_Chat1");

                entity.HasOne(d => d.UserUser)
                    .WithMany(p => p.ChatMembers)
                    .HasForeignKey(d => d.UserUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_User_has_Chat_User1");
            });

            modelBuilder.Entity<Friend>(entity =>
            {
                entity.HasKey(e => new { e.UserUserId, e.FriendId })
                    .HasName("PRIMARY")
                    .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

                entity.ToTable("friends");

                entity.HasIndex(e => e.FriendId, "fk_User_has_User_User1_idx");

                entity.HasIndex(e => e.UserUserId, "fk_User_has_User_User_idx");

                entity.Property(e => e.UserUserId).HasColumnName("User_user_id");

                entity.Property(e => e.FriendId).HasColumnName("friend_id");

                entity.HasOne(d => d.FriendNavigation)
                    .WithMany(p => p.FriendFriendNavigations)
                    .HasForeignKey(d => d.FriendId)
                    .HasConstraintName("fk_User_has_User_User1");

                entity.HasOne(d => d.UserUser)
                    .WithMany(p => p.FriendUserUsers)
                    .HasForeignKey(d => d.UserUserId)
                    .HasConstraintName("fk_User_has_User_User");
            });

            modelBuilder.Entity<Like>(entity =>
            {
                entity.ToTable("likes");

                entity.HasIndex(e => e.MessageMessageId, "fk_Likes_Message1_idx");

                entity.HasIndex(e => e.PostPostId, "fk_Likes_Post1_idx");

                entity.HasIndex(e => e.UserUserId, "fk_User_has_Post_User1_idx");

                entity.Property(e => e.LikeId).HasColumnName("like_id");

                entity.Property(e => e.MessageMessageId).HasColumnName("Message_message_id");

                entity.Property(e => e.PostPostId).HasColumnName("Post_post_id");

                entity.Property(e => e.UserUserId).HasColumnName("User_user_id");

                entity.HasOne(d => d.MessageMessage)
                    .WithMany(p => p.Likes)
                    .HasForeignKey(d => d.MessageMessageId)
                    .HasConstraintName("fk_Likes_Message1");

                entity.HasOne(d => d.PostPost)
                    .WithMany(p => p.Likes)
                    .HasPrincipalKey(p => p.PostId)
                    .HasForeignKey(d => d.PostPostId)
                    .HasConstraintName("fk_Likes_Post1");

                entity.HasOne(d => d.UserUser)
                    .WithMany(p => p.Likes)
                    .HasForeignKey(d => d.UserUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_User_has_Post_User1");
            });

            modelBuilder.Entity<Message>(entity =>
            {
                entity.ToTable("message");

                entity.HasIndex(e => e.ChatChatId, "fk_Message_Chat1_idx");

                entity.HasIndex(e => e.PostPostId, "fk_Message_Post1_idx");

                entity.HasIndex(e => e.UserUserId, "fk_Message_User1_idx");

                entity.HasIndex(e => e.MessageId, "message_id_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.MessageId).HasColumnName("message_id");

                entity.Property(e => e.ChatChatId).HasColumnName("Chat_chat_id");

                entity.Property(e => e.DateSending)
                    .HasColumnType("datetime")
                    .HasColumnName("Date_sending");

                entity.Property(e => e.PostPostId).HasColumnName("Post_post_id");

                entity.Property(e => e.Text).HasMaxLength(255);

                entity.Property(e => e.UserUserId).HasColumnName("User_user_id");

                entity.HasOne(d => d.ChatChat)
                    .WithMany(p => p.Messages)
                    .HasForeignKey(d => d.ChatChatId)
                    .HasConstraintName("fk_Message_Chat1");

                entity.HasOne(d => d.PostPost)
                    .WithMany(p => p.Messages)
                    .HasPrincipalKey(p => p.PostId)
                    .HasForeignKey(d => d.PostPostId)
                    .HasConstraintName("fk_Message_Post1");

                entity.HasOne(d => d.UserUser)
                    .WithMany(p => p.Messages)
                    .HasForeignKey(d => d.UserUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_Message_User1");
            });

            modelBuilder.Entity<Post>(entity =>
            {
                entity.HasKey(e => new { e.PostId, e.UserUserId })
                    .HasName("PRIMARY")
                    .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

                entity.ToTable("post");

                entity.HasIndex(e => e.UserUserId, "fk_Post_User1_idx");

                entity.HasIndex(e => e.PostId, "post_id_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.PostId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("post_id");

                entity.Property(e => e.UserUserId).HasColumnName("User_user_id");

                entity.Property(e => e.DatePosting)
                    .HasColumnType("datetime")
                    .HasColumnName("Date_posting");

                entity.Property(e => e.ImageReference)
                    .HasMaxLength(100)
                    .HasColumnName("Image_reference");

                entity.Property(e => e.Text).HasMaxLength(255);

                entity.HasOne(d => d.UserUser)
                    .WithMany(p => p.Posts)
                    .HasForeignKey(d => d.UserUserId)
                    .HasConstraintName("fk_Post_User1");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("user");

                entity.HasIndex(e => e.Name, "Name_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.UserId, "user_id_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.Property(e => e.Description).HasMaxLength(255);

                entity.Property(e => e.Email).HasMaxLength(40);

                entity.Property(e => e.ImageReference)
                    .HasMaxLength(100)
                    .HasColumnName("Image_reference");

                entity.Property(e => e.IsAdmin).HasColumnName("Is_admin");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(45);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(20);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
