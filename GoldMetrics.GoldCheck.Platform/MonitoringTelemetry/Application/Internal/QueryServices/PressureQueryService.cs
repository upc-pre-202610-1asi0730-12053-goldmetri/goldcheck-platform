using GoldMetrics.GoldCheck.Platform.MonitoringTelemetry.Application.QueryServices;
using GoldMetrics.GoldCheck.Platform.MonitoringTelemetry.Domain.Model.Aggregates;
using GoldMetrics.GoldCheck.Platform.MonitoringTelemetry.Domain.Model.Queries;
using GoldMetrics.GoldCheck.Platform.MonitoringTelemetry.Domain.Repositories;

namespace GoldMetrics.GoldCheck.Platform.MonitoringTelemetry.Application.Internal.QueryServices;

public class PressureQueryService(IPressureReadingRepository repository) : IPressureQueryService
{
    public async Task<IEnumerable<PressureReading>> Handle(GetPressureReadingByAssetQuery query, CancellationToken cancellationToken = default)
        => await repository.FindByAssetIdAsync(query.AssetId, cancellationToken);
    public async Task<IEnumerable<PressureReading>> Handle(GetPressureAnomaliesByAssetQuery query, CancellationToken cancellationToken = default)
    {
        var readings = await repository.FindByAssetIdAsync(query.AssetId, cancellationToken);
        return readings.Where(r => r.AnomalyPressureType is not null);
    }
}