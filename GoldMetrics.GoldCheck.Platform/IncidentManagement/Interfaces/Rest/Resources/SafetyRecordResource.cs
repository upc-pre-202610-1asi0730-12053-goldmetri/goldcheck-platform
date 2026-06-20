namespace GoldMetrics.GoldCheck.Platform.IncidentManagement.Interfaces.Rest.Resources;

public record SafetyRecordResource(
    int Id,
    string IncidentType,
    string OperatorId,
    string AssetId,
    string RiskLevel,
    string? Description,
    string Status);