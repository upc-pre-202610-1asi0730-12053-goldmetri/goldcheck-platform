using GoldMetrics.GoldCheck.Platform.MonitoringTelemetry.Domain.Model.Aggregates;
using GoldMetrics.GoldCheck.Platform.MonitoringTelemetry.Domain.Model.Commands;
using GoldMetrics.GoldCheck.Platform.Shared.Application.Model;

namespace GoldMetrics.GoldCheck.Platform.MonitoringTelemetry.Application.CommandServices;

public interface ISpeedCommandService
{
    Task<Result<SpeedReading>> Handle(MonitorSpeedStatusCommand command, CancellationToken cancellationToken = default);
    Task<Result<SpeedReading>> Handle(DetectSpeedExcessCommand command, CancellationToken cancellationToken = default);

}