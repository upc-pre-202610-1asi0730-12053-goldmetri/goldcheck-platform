using GoldMetrics.GoldCheck.Platform.MonitoringTelemetry.Domain.Model.Aggregates;
using GoldMetrics.GoldCheck.Platform.MonitoringTelemetry.Domain.Model.Commands;
using GoldMetrics.GoldCheck.Platform.Shared.Application.Model;

namespace GoldMetrics.GoldCheck.Platform.MonitoringTelemetry.Application.CommandServices;

public interface IGNSSCommandService
{
    Task<Result<GNSSStatus>> Handle(MonitorGNSSStatusCommand command, CancellationToken cancellationToken = default);
    Task<Result<GNSSStatus>> Handle(DetectGNSSAnomalyCommand command, CancellationToken cancellationToken = default);

}