using GoldMetrics.GoldCheck.Platform.Shared.Domain.Model.Entities;

namespace GoldMetrics.GoldCheck.Platform.IncidentManagement.Domain.Model.Aggregates;

public partial class SafetyRecord : IAuditableEntity
{
    public DateTimeOffset? CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
}