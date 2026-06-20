using GoldMetrics.GoldCheck.Platform.MonitoringTelemetry.Domain.Model.Aggregates;
using GoldMetrics.GoldCheck.Platform.MonitoringTelemetry.Domain.Model.Queries;

namespace GoldMetrics.GoldCheck.Platform.MonitoringTelemetry.Application.QueryServices;

public interface ITemperatureQueryService
{
    Task<IEnumerable<TemperatureReading>> Handle(GetTemperatureReadingByAssetQuery query, CancellationToken cancellationToken = default);
    Task<IEnumerable<TemperatureReading>> Handle(GetAllTemperatureReadingsQuery query, CancellationToken cancellationToken = default);
}