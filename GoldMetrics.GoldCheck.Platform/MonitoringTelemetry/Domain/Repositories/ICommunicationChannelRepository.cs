using GoldMetrics.GoldCheck.Platform.MonitoringTelemetry.Domain.Model.Aggregates;
using GoldMetrics.GoldCheck.Platform.Shared.Domain.Repositories;

namespace GoldMetrics.GoldCheck.Platform.MonitoringTelemetry.Domain.Repositories;

public interface ICommunicationChannelRepository : IBaseRepository<CommunicationChannel>
{
    Task<CommunicationChannel?> FindByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<IEnumerable<CommunicationChannel>> FindByAssetIdAsync(string assetId, CancellationToken cancellationToken = default);
    Task<IEnumerable<CommunicationChannel>> FindAllAsync(CancellationToken cancellationToken = default);
}