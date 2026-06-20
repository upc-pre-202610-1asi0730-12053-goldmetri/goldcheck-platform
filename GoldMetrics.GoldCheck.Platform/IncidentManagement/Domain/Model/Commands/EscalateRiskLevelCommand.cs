namespace GoldMetrics.GoldCheck.Platform.IncidentManagement.Domain.Model.Commands;

public record EscalateRiskLevelCommand(int Id, string NewRiskLevel);