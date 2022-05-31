using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FaceSpam_social_media.Infrastructure.Data;

namespace FaceSpam_social_media.Infrastructure.Configuration
{
    public class ChatConfiguration : IEntityTypeConfiguration<Chat>
    {
        public void Configure(EntityTypeBuilder<Chat> builder)
        {
            builder.ToTable("chat");

            builder.Property(e => e.Id/*ChatId*/).HasColumnName("chat_id");

            builder.Property(e => e.ChatName)
                .IsRequired()
                .HasMaxLength(45)
                .HasColumnName("Chat_name");

            builder.Property(e => e.DateCreating)
                .HasColumnType("datetime")
                .HasColumnName("Date_creating");

            builder.Property(e => e.Description).HasMaxLength(255);

            builder.Property(e => e.ImageReference)
                .HasMaxLength(100)
                .HasColumnName("Image_reference");
        }
    }
}