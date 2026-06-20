using GoldMetrics.GoldCheck.Platform.MonitoringTelemetry.Domain.Model.Aggregates;
using GoldMetrics.GoldCheck.Platform.MonitoringTelemetry.Domain.Model.Commands;
using GoldMetrics.GoldCheck.Platform.Shared.Application.Model;

namespace GoldMetrics.GoldCheck.Platform.MonitoringTelemetry.Application.CommandServices;

public interface ITemperatureCommandService
{
    Task<Result<TemperatureReading>> Handle(MonitorEngineTemperatureCommand command, CancellationToken cancellationToken = default);
    Task<Result<TemperatureReading>> Handle(AnalyseExhaustTemperatureCommand command, CancellationToken cancellationToken = default);
    Task<Result<TemperatureReading>> Handle(AnalyseExhaustTemperatureLimitPerCylinderCommand command, CancellationToken cancellationToken = default);
    Task<Result<TemperatureReading>> Handle(AnalyseEngineRefrigerantTemperatureCommand command, CancellationToken cancellationToken = default);
}