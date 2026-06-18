namespace GoldMetrics.GoldCheck.Platform.Analytics.Domain.Model.Commands;

public record RequestProductionDataCommand(string SupervisorId, DateTimeOffset Start, DateTimeOffset End);