namespace GoldMetrics.GoldCheck.Platform.MonitoringTelemetry.Interfaces.Acl;

public interface IMonitoringTelemetryContextFacade
{
    Task<bool> ValidateTelemetryExistsByAssetId(string assetId, CancellationToken cancellationToken);
    Task<string> FetchLatestTelemetryStatusByAssetId(string assetId, CancellationToken cancellationToken);
}
