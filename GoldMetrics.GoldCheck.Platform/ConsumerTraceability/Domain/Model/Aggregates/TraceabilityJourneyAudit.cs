using GoldMetrics.GoldCheck.Platform.Shared.Domain.Model.Entities;

namespace GoldMetrics.GoldCheck.Platform.ConsumerTraceability.Domain.Model.Aggregates;

public partial class TraceabilityJourney : IAuditableEntity
{
    public DateTimeOffset? CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
}