using GoldMetrics.GoldCheck.Platform.MonitoringTelemetry.Domain.Model.Aggregates;
using GoldMetrics.GoldCheck.Platform.MonitoringTelemetry.Domain.Model.Queries;

namespace GoldMetrics.GoldCheck.Platform.MonitoringTelemetry.Application.QueryServices;

public interface ICommunicationQueryService
{
    Task<IEnumerable<CommunicationChannel>> Handle(GetCommunicationChannelByAssetQuery query, CancellationToken cancellationToken = default);
    Task<IEnumerable<CommunicationChannel>> Handle(GetCommunicationAnomaliesByAssetQuery query, CancellationToken cancellationToken = default);
}