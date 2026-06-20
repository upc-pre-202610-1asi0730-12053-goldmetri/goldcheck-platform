using GoldMetrics.GoldCheck.Platform.MonitoringTelemetry.Domain.Model.Aggregates;
using GoldMetrics.GoldCheck.Platform.Shared.Domain.Repositories;

namespace GoldMetrics.GoldCheck.Platform.MonitoringTelemetry.Domain.Repositories;

public interface ISpeedReadingRepository : IBaseRepository<SpeedReading>
{
    Task<SpeedReading?> FindByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<IEnumerable<SpeedReading>> FindByAssetIdAsync(string assetId, CancellationToken cancellationToken = default);
    Task<IEnumerable<SpeedReading>> FindAllAsync(CancellationToken cancellationToken = default);
}