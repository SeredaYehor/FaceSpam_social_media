using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FaceSpam_social_media.Infrastructure.Data;

namespace FaceSpam_social_media.Infrastructure.Configuration
{
    public class ChatMemberConfiguration : IEntityTypeConfiguration<ChatMember>
    {
        public void Configure(EntityTypeBuilder<ChatMember> builder)
        {
            builder.HasIndex(e => e.Id, "id_UNIQUE")
                .IsUnique();

            builder.Property(e => e.Id).HasColumnName("id");

            builder.HasKey(e => new { e.UserUserId, e.ChatChatId })
                    .HasName("PRIMARY")
                    .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

            builder.ToTable("chat_members");

            builder.HasIndex(e => e.ChatChatId, "fk_User_has_Chat_Chat1_idx");

            builder.HasIndex(e => e.UserUserId, "fk_User_has_Chat_User1_idx");

            builder.Property(e => e.UserUserId).HasColumnName("User_user_id");

            builder.Property(e => e.ChatChatId).HasColumnName("Chat_chat_id");

            builder.HasOne(d => d.ChatChat)
                .WithMany(p => p.ChatMembers)
                .HasForeignKey(d => d.ChatChatId)
                .HasConstraintName("fk_User_has_Chat_Chat1");

            builder.HasOne(d => d.UserUser)
                .WithMany(p => p.ChatMembers)
                .HasForeignKey(d => d.UserUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_User_has_Chat_User1");
        }
    }
}