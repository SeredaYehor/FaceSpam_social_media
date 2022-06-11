using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FaceSpam_social_media.Infrastructure.Data;

namespace FaceSpam_social_media.Infrastructure.Configuration
{
    public class FriendConfiguration : IEntityTypeConfiguration<Friend>
    {
        public void Configure(EntityTypeBuilder<Friend> builder)
        {
            builder.HasKey(e => new { e.UserUserId, e.FriendId })
                     .HasName("PRIMARY")
                     .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

            builder.ToTable("friends");

            builder.HasIndex(e => e.FriendId, "fk_User_has_User_User1_idx");

            builder.HasIndex(e => e.UserUserId, "fk_User_has_User_User_idx");
            
            builder.Property(e => e.Id).HasColumnName("friendship_id");

            builder.Property(e => e.UserUserId).HasColumnName("User_user_id");

            builder.Property(e => e.FriendId).HasColumnName("friend_id");

            builder.HasOne(d => d.FriendNavigation)
                .WithMany(p => p.FriendFriendNavigations)
                .HasForeignKey(d => d.FriendId)
                .HasConstraintName("fk_User_has_User_User1");

            builder.HasOne(d => d.UserUser)
                .WithMany(p => p.FriendUserUsers)
                .HasForeignKey(d => d.UserUserId)
                .HasConstraintName("fk_User_has_User_User");
        }
    }
}