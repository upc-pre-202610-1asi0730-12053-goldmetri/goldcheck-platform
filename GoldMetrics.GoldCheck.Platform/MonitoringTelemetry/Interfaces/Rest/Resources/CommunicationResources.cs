namespace GoldMetrics.GoldCheck.Platform.MonitoringTelemetry.Interfaces.Rest.Resources;

public record MonitorCommunicationChannelResource(string AssetId);

public record CommunicationChannelResource(
    int Id,
    string AssetId,
    string Status,
    string? LastAnalysedProtocol,
    string? AnomalyProtocol,
    string? AnomalyDescription,
    DateTimeOffset? CreatedAt,
    DateTimeOffset? UpdatedAt);