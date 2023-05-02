using FaceSpam.DAL.DataBase.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FaceSpam.DAL.DataBase.EntityConfigurations;

public class FollowingConfiguration : IEntityTypeConfiguration<Following>
{
    public void Configure(EntityTypeBuilder<Following> builder)
    {
        builder
            .HasKey(x => new { x.FollowerId, x.FollowingId });

        //builder
        //    .HasOne(x => x.Follower)
        //    .WithMany(x => x.UserFollowings)
        //    .HasForeignKey(x => x.FollowerId);

        //builder
        //    .HasOne(x => x.Followed)
        //    .WithMany(x => x.UserFollowers)
        //    .HasForeignKey(x => x.FollowingId);
    }
}