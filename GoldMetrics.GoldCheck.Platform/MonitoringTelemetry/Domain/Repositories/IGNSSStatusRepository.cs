using GoldMetrics.GoldCheck.Platform.MonitoringTelemetry.Domain.Model.Aggregates;
using GoldMetrics.GoldCheck.Platform.Shared.Domain.Repositories;

namespace GoldMetrics.GoldCheck.Platform.MonitoringTelemetry.Domain.Repositories;

public interface IGNSSStatusRepository : IBaseRepository<GNSSStatus>
{
    Task<GNSSStatus?> FindByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<IEnumerable<GNSSStatus>> FindByAssetIdAsync(string assetId, CancellationToken cancellationToken = default);
    Task<IEnumerable<GNSSStatus>> FindAllAsync(CancellationToken cancellationToken = default);
}