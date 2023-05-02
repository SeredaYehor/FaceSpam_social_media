using FaceSpam.DAL.Entities.ChatEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FaceSpam.DAL.EntityConfigs;

public class ChatConfiguration : IEntityTypeConfiguration<Chat>
{
    public void Configure(EntityTypeBuilder<Chat> builder)
    {
        builder
            .Property(x => x.Name)
            .HasColumnType("nvarchar(50)")
            .IsRequired();

        builder
            .Property(x => x.Description)
            .HasColumnType("nvarchar(255)")
            .IsRequired(false);

        builder
            .Property(x => x.ImageReference)
            .HasColumnType("nvarchar(255)");

        builder
            .Property(x => x.DateOfCreating);
    }
}