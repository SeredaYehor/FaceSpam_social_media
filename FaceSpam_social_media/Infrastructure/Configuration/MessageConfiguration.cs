using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FaceSpam_social_media.Infrastructure.Data;

namespace FaceSpam_social_media.Infrastructure.Configuration
{
    public class MessageConfiguration : IEntityTypeConfiguration<Message>
    {
        public void Configure(EntityTypeBuilder<Message> builder)
        {
            builder.ToTable("message");

            builder.HasIndex(e => e.ChatChatId, "fk_Message_Chat1_idx");

            builder.HasIndex(e => e.PostPostId, "fk_Message_Post1_idx");

            builder.HasIndex(e => e.UserUserId, "fk_Message_User1_idx");

            builder.HasIndex(e => e.Id/*MessageId*/, "message_id_UNIQUE")
                .IsUnique();

            builder.Property(e => e.Id/*MessageId*/).HasColumnName("message_id");

            builder.Property(e => e.ChatChatId).HasColumnName("Chat_chat_id");

            builder.Property(e => e.DateSending)
                .HasColumnType("datetime")
                .HasColumnName("Date_sending");

            builder.Property(e => e.PostPostId).HasColumnName("Post_post_id");

            builder.Property(e => e.Text).HasMaxLength(255);

            builder.Property(e => e.UserUserId).HasColumnName("User_user_id");

            builder.HasOne(d => d.ChatChat)
                .WithMany(p => p.Messages)
                .HasForeignKey(d => d.ChatChatId)
                .HasConstraintName("fk_Message_Chat1");

            builder.HasOne(d => d.PostPost)
                .WithMany(p => p.Messages)
                .HasPrincipalKey(p => p.Id/*PostId*/)
                .HasForeignKey(d => d.PostPostId)
                .HasConstraintName("fk_Message_Post1");

            builder.HasOne(d => d.UserUser)
                .WithMany(p => p.Messages)
                .HasForeignKey(d => d.UserUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Message_User1");
        }
    }
}