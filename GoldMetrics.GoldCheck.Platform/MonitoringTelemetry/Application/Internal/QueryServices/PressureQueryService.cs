using GoldMetrics.GoldCheck.Platform.MonitoringTelemetry.Application.QueryServices;
using GoldMetrics.GoldCheck.Platform.MonitoringTelemetry.Domain.Model.Aggregates;
using GoldMetrics.GoldCheck.Platform.MonitoringTelemetry.Domain.Model.Queries;
using GoldMetrics.GoldCheck.Platform.MonitoringTelemetry.Domain.Repositories;

namespace GoldMetrics.GoldCheck.Platform.MonitoringTelemetry.Application.Internal.QueryServices;

public class PressureQueryService(IPressureReadingRepository repository) : IPressureQueryService
{
    public async Task<IEnumerable<PressureReading>> Handle(GetPressureReadingByAssetQuery query, CancellationToken cancellationToken = default)
        => await repository.FindByAssetIdAsync(query.AssetId, cancellationToken);
}