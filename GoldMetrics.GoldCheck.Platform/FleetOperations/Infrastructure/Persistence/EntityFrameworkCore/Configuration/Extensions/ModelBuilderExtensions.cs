using GoldMetrics.GoldCheck.Platform.FleetOperations.Domain.Model.Aggregates;
using Microsoft.EntityFrameworkCore;

namespace GoldMetrics.GoldCheck.Platform.FleetOperations.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;

public static class ModelBuilderExtensions
{
    public static void ApplyFleetOperationsConfiguration(this ModelBuilder builder)
    {
        // Vehicle
        builder.Entity<Vehicle>().HasKey(v => v.Id);
        builder.Entity<Vehicle>().Property(v => v.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Entity<Vehicle>().OwnsOne(v => v.VehicleId, vid =>
        {
            vid.WithOwner().HasForeignKey("Id");
            vid.Property(v => v.Value).HasColumnName("VehicleId").IsRequired().HasMaxLength(100);
        });
        builder.Entity<Vehicle>().OwnsOne(v => v.OperatorId, oid =>
        {
            oid.WithOwner().HasForeignKey("Id");
            oid.Property(o => o.Value).HasColumnName("OperatorId").IsRequired().HasMaxLength(100);
        });
        builder.Entity<Vehicle>().Property(v => v.IsEngineOn).IsRequired();
        builder.Entity<Vehicle>().Property(v => v.Status).IsRequired().HasMaxLength(50);
    }
}
