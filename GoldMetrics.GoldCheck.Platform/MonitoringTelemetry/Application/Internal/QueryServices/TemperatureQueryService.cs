using GoldMetrics.GoldCheck.Platform.MonitoringTelemetry.Application.QueryServices;
using GoldMetrics.GoldCheck.Platform.MonitoringTelemetry.Domain.Model.Aggregates;
using GoldMetrics.GoldCheck.Platform.MonitoringTelemetry.Domain.Model.Queries;
using GoldMetrics.GoldCheck.Platform.MonitoringTelemetry.Domain.Repositories;

namespace GoldMetrics.GoldCheck.Platform.MonitoringTelemetry.Application.Internal.QueryServices;

public class TemperatureQueryService(ITemperatureReadingRepository repository) : ITemperatureQueryService
{
    public async Task<IEnumerable<TemperatureReading>> Handle(GetTemperatureReadingByAssetQuery query, CancellationToken cancellationToken = default)
        => await repository.FindByAssetIdAsync(query.AssetId, cancellationToken);
    public async Task<IEnumerable<TemperatureReading>> Handle(GetAllTemperatureReadingsQuery query, CancellationToken cancellationToken = default)
        => await repository.FindAllAsync(cancellationToken);
    
    public async Task<IEnumerable<TemperatureReading>> Handle(GetTemperatureAnomaliesByAssetQuery query, CancellationToken cancellationToken = default)
    {
        var readings = await repository.FindByAssetIdAsync(query.AssetId, cancellationToken);
        return readings.Where(r => r.AnomalyType is not null);
    }
}