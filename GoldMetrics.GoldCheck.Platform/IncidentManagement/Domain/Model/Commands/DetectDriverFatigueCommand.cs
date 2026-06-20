namespace GoldMetrics.GoldCheck.Platform.IncidentManagement.Domain.Model.Commands;

public record DetectDriverFatigueCommand(string OperatorId, string AssetId);