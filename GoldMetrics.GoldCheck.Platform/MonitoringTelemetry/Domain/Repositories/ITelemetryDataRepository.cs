using GoldMetrics.GoldCheck.Platform.MonitoringTelemetry.Domain.Model.Aggregates;
using GoldMetrics.GoldCheck.Platform.Shared.Domain.Repositories;

namespace GoldMetrics.GoldCheck.Platform.MonitoringTelemetry.Domain.Repositories;

public interface ITelemetryDataRepository : IBaseRepository<TelemetryData>
{
    Task<TelemetryData?> FindByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<TelemetryData?> FindByTelemetryDataIdAsync(string telemetryDataId, CancellationToken cancellationToken = default);
    Task<IEnumerable<TelemetryData>> FindByAssetIdAsync(string assetId, CancellationToken cancellationToken = default);
    Task<IEnumerable<TelemetryData>> FindAllAsync(CancellationToken cancellationToken = default);
}