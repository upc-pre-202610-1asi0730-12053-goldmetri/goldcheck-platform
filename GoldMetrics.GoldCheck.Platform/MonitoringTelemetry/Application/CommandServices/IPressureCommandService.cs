using GoldMetrics.GoldCheck.Platform.MonitoringTelemetry.Domain.Model.Aggregates;
using GoldMetrics.GoldCheck.Platform.MonitoringTelemetry.Domain.Model.Commands;
using GoldMetrics.GoldCheck.Platform.Shared.Application.Model;

namespace GoldMetrics.GoldCheck.Platform.MonitoringTelemetry.Application.CommandServices;

public interface IPressureCommandService
{
    Task<Result<PressureReading>> Handle(MonitorPressureCommand command, CancellationToken cancellationToken = default);
    Task<Result<PressureReading>> Handle(AnalysePressureCommand command, CancellationToken cancellationToken = default);
    Task<Result<PressureReading>> Handle(DetectPressureAnomalyCommand command, CancellationToken cancellationToken = default);

}