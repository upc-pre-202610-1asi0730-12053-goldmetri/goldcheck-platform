using GoldMetrics.GoldCheck.Platform.MonitoringTelemetry.Application.QueryServices;
using GoldMetrics.GoldCheck.Platform.MonitoringTelemetry.Domain.Model.Queries;
using GoldMetrics.GoldCheck.Platform.MonitoringTelemetry.Interfaces.Acl;

namespace GoldMetrics.GoldCheck.Platform.MonitoringTelemetry.Application.Acl;

public class MonitoringTelemetryContextFacade(ITelemetryQueryService telemetryQueryService)
    : IMonitoringTelemetryContextFacade
{
    public async Task<bool> ValidateTelemetryExistsByAssetId(string assetId, CancellationToken cancellationToken)
    {
        var query = new GetTelemetryDataByAssetQuery(assetId);
        var result = await telemetryQueryService.Handle(query, cancellationToken);
        return result.Any();
    }

    public async Task<string> FetchLatestTelemetryStatusByAssetId(string assetId, CancellationToken cancellationToken)
    {
        var query = new GetTelemetryDataByAssetQuery(assetId);
        var result = await telemetryQueryService.Handle(query, cancellationToken);
        return result.FirstOrDefault()?.Status ?? string.Empty;
    }
}
