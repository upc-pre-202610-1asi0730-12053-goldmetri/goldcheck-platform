using GoldMetrics.GoldCheck.Platform.MonitoringTelemetry.Domain.Model.Aggregates;
using GoldMetrics.GoldCheck.Platform.MonitoringTelemetry.Domain.Model.Queries;

namespace GoldMetrics.GoldCheck.Platform.MonitoringTelemetry.Application.QueryServices;

public interface IPressureQueryService
{
    Task<IEnumerable<PressureReading>> Handle(GetPressureReadingByAssetQuery query, CancellationToken cancellationToken = default);
}