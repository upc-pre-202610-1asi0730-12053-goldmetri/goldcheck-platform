using GoldMetrics.GoldCheck.Platform.MonitoringTelemetry.Domain.Model.Aggregates;
using GoldMetrics.GoldCheck.Platform.Shared.Domain.Repositories;

namespace GoldMetrics.GoldCheck.Platform.MonitoringTelemetry.Domain.Repositories;

public interface ITemperatureReadingRepository : IBaseRepository<TemperatureReading>
{
    Task<TemperatureReading?> FindByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<IEnumerable<TemperatureReading>> FindByAssetIdAsync(string assetId, CancellationToken cancellationToken = default);
    Task<IEnumerable<TemperatureReading>> FindAllAsync(CancellationToken cancellationToken = default);
}