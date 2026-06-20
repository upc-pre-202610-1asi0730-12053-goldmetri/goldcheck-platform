using GoldMetrics.GoldCheck.Platform.MonitoringTelemetry.Domain.Model.Aggregates;
using GoldMetrics.GoldCheck.Platform.MonitoringTelemetry.Domain.Model.Queries;

namespace GoldMetrics.GoldCheck.Platform.MonitoringTelemetry.Application.QueryServices;

public interface ISpeedQueryService
{
    Task<IEnumerable<SpeedReading>> Handle(GetSpeedReadingByAssetQuery query, CancellationToken cancellationToken = default);
    Task<IEnumerable<SpeedReading>> Handle(GetSpeedViolationsByAssetQuery query, CancellationToken cancellationToken = default);

}