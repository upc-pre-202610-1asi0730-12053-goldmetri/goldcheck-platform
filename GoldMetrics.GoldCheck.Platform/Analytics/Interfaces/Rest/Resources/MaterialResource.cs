namespace GoldMetrics.GoldCheck.Platform.Analytics.Interfaces.Rest.Resources;

public record MaterialResource(
    int Id,
    string MaterialId,
    string RouteId,
    string RouteStatus,
    string SupervisorId,
    string UserId,
    DateTimeOffset? ProductionStart, 
    DateTimeOffset? ProductionEnd,
    decimal? ProductionTons,
    string Status);