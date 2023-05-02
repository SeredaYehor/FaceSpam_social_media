using FaceSpam.DAL.DataBase.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FaceSpam.DAL.DataBase.EntityConfigurations;

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

        builder
            .HasMany(x => x.Members)
            .WithMany(x => x.Chats);
    }
}