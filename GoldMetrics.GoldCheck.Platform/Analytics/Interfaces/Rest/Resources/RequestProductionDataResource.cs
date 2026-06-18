namespace GoldMetrics.GoldCheck.Platform.Analytics.Interfaces.Rest.Resources;

public record RequestProductionDataResource(string SupervisorId, DateTimeOffset Start, DateTimeOffset End);