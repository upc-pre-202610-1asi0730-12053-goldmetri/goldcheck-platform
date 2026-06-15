using GoldMetrics.GoldCheck.Platform.ReportingNotifications.Domain.Model.Aggregates;
using GoldMetrics.GoldCheck.Platform.ReportingNotifications.Interfaces.Rest.Resources;

namespace GoldMetrics.GoldCheck.Platform.ReportingNotifications.Interfaces.Rest.Transform;

public static class ReportResourceFromEntityAssembler
{
    public static ReportResource ToResourceFromEntity(Report entity) =>
        new(entity.Id, entity.SupervisorId.Value, entity.IncidentId,
            entity.ReportStatus.Value, entity.ReportFormat.Value, entity.DownloadedByUserId, entity.Status);
}