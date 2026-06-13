using GoldMetrics.GoldCheck.Platform.Shared.Domain.Model.Entities;

namespace GoldMetrics.GoldCheck.Platform.ReportingNotifications.Domain.Model.Aggregates;

public partial class Report : IAuditableEntity
{
    public DateTimeOffset? CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
}