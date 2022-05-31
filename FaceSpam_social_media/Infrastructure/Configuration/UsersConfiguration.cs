using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FaceSpam_social_media.Infrastructure.Data;

namespace FaceSpam_social_media.Infrastructure.Configuration
{
    public class UsersConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("user");

            builder.HasIndex(e => e.Name, "Name_UNIQUE")
                .IsUnique();

            builder.HasIndex(e => e.Id, "user_id_UNIQUE")
                .IsUnique();

            builder.Property(e => e.Id).HasColumnName("user_id");

            builder.Property(e => e.Description).HasMaxLength(255);

            builder.Property(e => e.Email).HasMaxLength(40);

            builder.Property(e => e.ImageReference)
                .HasMaxLength(100)
                .HasColumnName("Image_reference");

            builder.Property(e => e.IsAdmin).HasColumnName("Is_admin");

            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(45);

            builder.Property(e => e.Password)
                .IsRequired()
                .HasMaxLength(20);
        }
    }
}