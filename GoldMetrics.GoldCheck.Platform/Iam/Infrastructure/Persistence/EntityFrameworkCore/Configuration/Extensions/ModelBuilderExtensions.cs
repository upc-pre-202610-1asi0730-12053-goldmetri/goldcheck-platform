using GoldMetrics.GoldCheck.Platform.Iam.Domain.Model.Aggregates;
using Microsoft.EntityFrameworkCore;

namespace GoldMetrics.GoldCheck.Platform.Iam.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;

public static class ModelBuilderExtensions
{
    public static ModelBuilder ApplyIAMConfiguration(this ModelBuilder builder)
    {
        builder.Entity<User>(entity =>
        {
            entity.HasKey(u => u.Id);

            entity.OwnsOne(u => u.Username, vo =>
            {
                vo.Property(v => v.Value).HasColumnName("username").IsRequired();
                vo.WithOwner().HasForeignKey("UserId");
                vo.HasKey("UserId").HasName("PK_Users_Username");
            });

            entity.OwnsOne(u => u.HashedPassword, vo =>
            {
                vo.Property(v => v.Value).HasColumnName("hashed_password").IsRequired();
                vo.WithOwner().HasForeignKey("UserId");
                vo.HasKey("UserId").HasName("PK_Users_HashedPassword");
            });

            entity.OwnsOne(u => u.Email, vo =>
            {
                vo.Property(v => v.Address).HasColumnName("email").IsRequired();
                vo.WithOwner().HasForeignKey("UserId");
                vo.HasKey("UserId").HasName("PK_Users_Email");
            });

            entity.OwnsOne(u => u.Role, vo =>
            {
                vo.Property(v => v.Value).HasColumnName("role").IsRequired();
                vo.WithOwner().HasForeignKey("UserId");
                vo.HasKey("UserId").HasName("PK_Users_Role");
            });

            entity.Property(u => u.Status).IsRequired();
        });
        return builder;
    }
}