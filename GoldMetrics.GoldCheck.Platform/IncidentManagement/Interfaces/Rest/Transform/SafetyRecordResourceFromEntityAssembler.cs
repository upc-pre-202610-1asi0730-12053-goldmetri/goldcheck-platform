using GoldMetrics.GoldCheck.Platform.IncidentManagement.Domain.Model.Aggregates;
using GoldMetrics.GoldCheck.Platform.IncidentManagement.Interfaces.Rest.Resources;

namespace GoldMetrics.GoldCheck.Platform.IncidentManagement.Interfaces.Rest.Transform;

public static class SafetyRecordResourceFromEntityAssembler
{
    public static SafetyRecordResource ToResourceFromEntity(SafetyRecord entity) =>
        new(
            entity.Id,
            entity.IncidentType.Value,
            entity.OperatorId.Value,
            entity.AssetId.Value,
            entity.RiskLevel.Value,
            entity.Description,
            entity.Status);
}