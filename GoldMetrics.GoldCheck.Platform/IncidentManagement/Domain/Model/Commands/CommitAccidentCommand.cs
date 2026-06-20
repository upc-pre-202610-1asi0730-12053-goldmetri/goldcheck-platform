namespace GoldMetrics.GoldCheck.Platform.IncidentManagement.Domain.Model.Commands;

public record CommitAccidentCommand(string OperatorId, string Description);