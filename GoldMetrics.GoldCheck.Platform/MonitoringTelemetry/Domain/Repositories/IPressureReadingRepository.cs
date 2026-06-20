using GoldMetrics.GoldCheck.Platform.MonitoringTelemetry.Domain.Model.Aggregates;
using GoldMetrics.GoldCheck.Platform.Shared.Domain.Repositories;

namespace GoldMetrics.GoldCheck.Platform.MonitoringTelemetry.Domain.Repositories;

public interface IPressureReadingRepository : IBaseRepository<PressureReading>
{
    Task<PressureReading?> FindByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<IEnumerable<PressureReading>> FindByAssetIdAsync(string assetId, CancellationToken cancellationToken = default);
    Task<IEnumerable<PressureReading>> FindAllAsync(CancellationToken cancellationToken = default);
}