using GoldMetrics.GoldCheck.Platform.MonitoringTelemetry.Domain.Model.Aggregates;
using GoldMetrics.GoldCheck.Platform.MonitoringTelemetry.Domain.Model.Commands;
using GoldMetrics.GoldCheck.Platform.Shared.Application.Model;

namespace GoldMetrics.GoldCheck.Platform.MonitoringTelemetry.Application.CommandServices;

public interface ICommunicationCommandService
{
    Task<Result<CommunicationChannel>> Handle(MonitorCommunicationChannelCommand command, CancellationToken cancellationToken = default);
    Task<Result<CommunicationChannel>> Handle(AnalyseCommunicationCommand command, CancellationToken cancellationToken = default);
}