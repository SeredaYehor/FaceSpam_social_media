using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FaceSpam_social_media.Infrastructure.Data;

namespace FaceSpam_social_media.Infrastructure.Configuration
{
    public class PostConfiguration : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.HasKey(e => new { e.Id/*PostId*/, e.UserUserId })
                    .HasName("PRIMARY")
                    .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

            builder.ToTable("post");

            builder.HasIndex(e => e.UserUserId, "fk_Post_User1_idx");

            builder.HasIndex(e => e.Id/*PostId*/, "post_id_UNIQUE")
                .IsUnique();

            builder.Property(e => e.Id/*PostId*/)
                .ValueGeneratedOnAdd()
                .HasColumnName("post_id");

            builder.Property(e => e.UserUserId).HasColumnName("User_user_id");

            builder.Property(e => e.DatePosting)
                .HasColumnType("datetime")
                .HasColumnName("Date_posting");

            builder.Property(e => e.ImageReference)
                .HasMaxLength(100)
                .HasColumnName("Image_reference");

            builder.Property(e => e.Text).HasMaxLength(255);

            builder.HasOne(d => d.UserUser)
                .WithMany(p => p.Posts)
                .HasForeignKey(d => d.UserUserId)
                .HasConstraintName("fk_Post_User1");
        }
       
    }
}
