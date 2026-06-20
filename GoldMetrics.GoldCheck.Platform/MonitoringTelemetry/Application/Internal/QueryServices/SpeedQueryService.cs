using GoldMetrics.GoldCheck.Platform.MonitoringTelemetry.Application.QueryServices;
using GoldMetrics.GoldCheck.Platform.MonitoringTelemetry.Domain.Model.Aggregates;
using GoldMetrics.GoldCheck.Platform.MonitoringTelemetry.Domain.Model.Queries;
using GoldMetrics.GoldCheck.Platform.MonitoringTelemetry.Domain.Repositories;

namespace GoldMetrics.GoldCheck.Platform.MonitoringTelemetry.Application.Internal.QueryServices;

public class SpeedQueryService(ISpeedReadingRepository repository) : ISpeedQueryService
{
    public async Task<IEnumerable<SpeedReading>> Handle(GetSpeedReadingByAssetQuery query, CancellationToken cancellationToken = default)
        => await repository.FindByAssetIdAsync(query.AssetId, cancellationToken);
    public async Task<IEnumerable<SpeedReading>> Handle(GetSpeedViolationsByAssetQuery query, CancellationToken cancellationToken = default)
    {
        var readings = await repository.FindByAssetIdAsync(query.AssetId, cancellationToken);
        return readings.Where(r => r.IsViolation);
    }
}