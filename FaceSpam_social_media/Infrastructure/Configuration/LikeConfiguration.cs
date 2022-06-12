using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FaceSpam_social_media.Infrastructure.Data;

namespace FaceSpam_social_media.Infrastructure.Configuration
{
    public class LikeConfiguration : IEntityTypeConfiguration<Like>
    {
        public void Configure(EntityTypeBuilder<Like> builder)
        {
            builder.ToTable("likes");

            builder.HasIndex(e => e.MessageMessageId, "fk_Likes_Message1_idx");

            builder.HasIndex(e => e.PostPostId, "fk_Likes_Post1_idx");

            builder.HasIndex(e => e.UserUserId, "fk_User_has_Post_User1_idx");

            builder.Property(e => e.Id/*LikeId*/).HasColumnName("like_id");

            builder.Property(e => e.MessageMessageId).HasColumnName("Message_message_id");

            builder.Property(e => e.PostPostId).HasColumnName("Post_post_id");

            builder.Property(e => e.UserUserId).HasColumnName("User_user_id");

            builder.HasOne(d => d.MessageMessage)
                .WithMany(p => p.Likes)
                .HasForeignKey(d => d.MessageMessageId)
                .HasConstraintName("fk_Likes_Message1");

            builder.HasOne(d => d.PostPost)
                .WithMany(p => p.Likes)
                .HasPrincipalKey(p => p.Id/*PostId*/)
                .HasForeignKey(d => d.PostPostId)
                .HasConstraintName("fk_Likes_Post1");

            builder.HasOne(d => d.UserUser)
                .WithMany(p => p.Likes)
                .HasForeignKey(d => d.UserUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_User_has_Post_User1");
        }
    }
}