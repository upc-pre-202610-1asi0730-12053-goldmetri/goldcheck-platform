namespace GoldMetrics.GoldCheck.Platform.MonitoringTelemetry.Interfaces.Rest.Resources;

public record ProcessTelemetryDataResource(string AssetId, string RawData);

public record TelemetryDataResource(
    int Id,
    string AssetId,
    string TelemetryDataId,
    string RawData,
    string Status,
    bool IsValidated,
    DateTimeOffset? CreatedAt,
    DateTimeOffset? UpdatedAt);
    
public record ValidateTelemetryDataResource(string AssetId, string TelemetryDataId);
