using FaceSpam.DAL.Entities.UserEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FaceSpam.DAL.EntityConfigs;

public class FollowerConfiguration : IEntityTypeConfiguration<Follower>
{
    public void Configure(EntityTypeBuilder<Follower> builder)
    {
        builder
            .HasOne(x => x.User)
            .WithMany(x => x.Followings)
            .HasForeignKey(x => x.UserId);

        builder
            .HasOne(x => x.Following)
            .WithMany(x => x.Followings)
            .HasForeignKey(x => x.FollowingId);
    }
}