using GoldMetrics.GoldCheck.Platform.Shared.Domain.Model.Entities;

namespace GoldMetrics.GoldCheck.Platform.FleetOperations.Domain.Model.Aggregates;

public partial class Vehicle : IAuditableEntity
{
    public DateTimeOffset? CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
}
