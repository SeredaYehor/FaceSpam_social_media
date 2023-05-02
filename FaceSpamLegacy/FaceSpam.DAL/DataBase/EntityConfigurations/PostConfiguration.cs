using FaceSpam.DAL.DataBase.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FaceSpam.DAL.DataBase.EntityConfigurations;

public class PostConfiguration : IEntityTypeConfiguration<Post>
{
    public void Configure(EntityTypeBuilder<Post> builder)
    {
        builder
            .Property(x => x.Text)
            .HasColumnType("text");

        builder
            .Property(x => x.ImageReference)
            .HasColumnType("nvarchar(255)");

        builder
            .HasOne(x => x.User)
            .WithMany(x => x.Posts)
            .HasForeignKey(x => x.UserId);
    }
}