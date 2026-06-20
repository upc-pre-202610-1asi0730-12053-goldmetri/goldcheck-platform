using GoldMetrics.GoldCheck.Platform.MonitoringTelemetry.Application.QueryServices;
using GoldMetrics.GoldCheck.Platform.MonitoringTelemetry.Domain.Model.Aggregates;
using GoldMetrics.GoldCheck.Platform.MonitoringTelemetry.Domain.Model.Queries;
using GoldMetrics.GoldCheck.Platform.MonitoringTelemetry.Domain.Repositories;

namespace GoldMetrics.GoldCheck.Platform.MonitoringTelemetry.Application.Internal.QueryServices;

public class CommunicationQueryService(ICommunicationChannelRepository repository) : ICommunicationQueryService
{
    public async Task<IEnumerable<CommunicationChannel>> Handle(GetCommunicationChannelByAssetQuery query, CancellationToken cancellationToken = default)
        => await repository.FindByAssetIdAsync(query.AssetId, cancellationToken);
    
    public async Task<IEnumerable<CommunicationChannel>> Handle(GetCommunicationAnomaliesByAssetQuery query, CancellationToken cancellationToken = default)
    {
        var channels = await repository.FindByAssetIdAsync(query.AssetId, cancellationToken);
        return channels.Where(c => c.AnomalyProtocol is not null);
    }
}