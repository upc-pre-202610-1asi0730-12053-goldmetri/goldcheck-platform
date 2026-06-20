using GoldMetrics.GoldCheck.Platform.MonitoringTelemetry.Domain.Model.Aggregates;
using GoldMetrics.GoldCheck.Platform.MonitoringTelemetry.Domain.Model.Commands;
using GoldMetrics.GoldCheck.Platform.Shared.Application.Model;

namespace GoldMetrics.GoldCheck.Platform.MonitoringTelemetry.Application.CommandServices;

public interface ITelemetryCommandService
{
    Task<Result<TelemetryData>> Handle(ProcessTelemetryDataCommand command, CancellationToken cancellationToken = default);
}