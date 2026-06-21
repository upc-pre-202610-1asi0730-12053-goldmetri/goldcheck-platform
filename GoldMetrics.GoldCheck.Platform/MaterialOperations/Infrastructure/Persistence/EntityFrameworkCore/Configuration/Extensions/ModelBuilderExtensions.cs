using GoldMetrics.GoldCheck.Platform.MaterialOperations.Domain.Model.Aggregates;
using Microsoft.EntityFrameworkCore;

namespace GoldMetrics.GoldCheck.Platform.MaterialOperations.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;

public static class ModelBuilderExtensions
{
    public static void ApplyMaterialOperationsConfiguration(this ModelBuilder builder)
    {
        builder.Entity<Material>().HasKey(m => m.Id);
        builder.Entity<Material>().Property(m => m.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Material>().ToTable("material_operations_materials");

        builder.Entity<Material>().OwnsOne(m => m.BatchId, b =>
        {
            b.WithOwner().HasForeignKey("Id");
            b.Property(x => x.Value).HasColumnName("batch_id").IsRequired().HasMaxLength(100);
        });

        builder.Entity<Material>().OwnsOne(m => m.MineralType, mt =>
        {
            mt.WithOwner().HasForeignKey("Id");
            mt.Property(x => x.Value).HasColumnName("mineral_type").IsRequired().HasMaxLength(50);
        });

        builder.Entity<Material>().OwnsOne(m => m.Payload, p =>
        {
            p.WithOwner().HasForeignKey("Id");
            p.Property(x => x.Tons).HasColumnName("payload_tons").IsRequired();
        });

        builder.Entity<Material>().Property(m => m.Status).IsRequired().HasMaxLength(50);

        builder.Entity<Material>().Property(m => m.Classification).HasColumnName("classification").HasMaxLength(200);

        builder.Entity<Material>().Property(m => m.DumpingPointName).HasColumnName("dumping_point_name").HasMaxLength(200);

        builder.Entity<Material>().Property(m => m.CurrentLocation).HasColumnName("current_location").HasMaxLength(300);
    }
}
