using FaceSpam.DAL.DataBase.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FaceSpam.DAL.DataBase.EntityConfigurations;

public class LikeConfiguration : IEntityTypeConfiguration<Like>
{
    public void Configure(EntityTypeBuilder<Like> builder)
    {
        builder
            .HasOne(x => x.Message)
            .WithMany(x => x.Likes)
            .HasForeignKey(x => x.MessageId);

        builder
            .HasOne(x => x.Post)
            .WithMany(x => x.Likes)
            .HasForeignKey(x => x.PostId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.ClientSetNull);

        builder
            .HasOne(x => x.User)
            .WithMany(x => x.Likes)
            .HasForeignKey(x => x.UserId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.ClientSetNull);
    }
}