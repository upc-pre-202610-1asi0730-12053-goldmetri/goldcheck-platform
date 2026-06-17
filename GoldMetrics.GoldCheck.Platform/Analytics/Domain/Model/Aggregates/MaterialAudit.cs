using GoldMetrics.GoldCheck.Platform.Shared.Domain.Model.Entities;

namespace GoldMetrics.GoldCheck.Platform.Analytics.Domain.Model.Aggregates;

public partial class Material : IAuditableEntity
{
    public DateTimeOffset? CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
}