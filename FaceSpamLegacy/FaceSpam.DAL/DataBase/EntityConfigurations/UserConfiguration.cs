using FaceSpam.DAL.DataBase.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FaceSpam.DAL.DataBase.EntityConfigurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder
            .HasIndex(x => x.Name)
            .IsUnique();

        builder
            .Property(x => x.Name)
            .IsRequired()
            .HasColumnType("nvarchar(50)");

        builder
            .Property(x => x.Password)
            .IsRequired()
            .HasColumnType("nvarchar(100)");

        builder
            .Property(x => x.Description)
            .HasColumnType("nvarchar(255)");

        builder
            .Property(x => x.Email)
            .HasColumnType("nvarchar(100)");

        builder
            .Property(x => x.ImageReference)
            .HasColumnType("nvarchar(255)");

        builder
            .Property(x => x.IsAdmin)
            .HasDefaultValue(false);

        builder
            .Property(x => x.IsBanned)
            .HasDefaultValue(false);

        builder
            .HasMany<User>()
            .WithMany(x => x.UserFollowings);
    }
}