using GoldMetrics.GoldCheck.Platform.IncidentManagement.Domain.Model.Aggregates;
using GoldMetrics.GoldCheck.Platform.IncidentManagement.Domain.Model.Commands;
using GoldMetrics.GoldCheck.Platform.Shared.Application.Model;

namespace GoldMetrics.GoldCheck.Platform.IncidentManagement.Application.CommandServices;

public interface IIncidentManagementCommandService
{
    Task<Result<SafetyRecord>> Handle(DetectDriverFatigueCommand command, CancellationToken cancellationToken);
    Task<Result<SafetyRecord>> Handle(EscalateRiskLevelCommand command, CancellationToken cancellationToken);
    Task<Result<SafetyRecord>> Handle(EvaluateSafetyRiskCommand command, CancellationToken cancellationToken);
    Task<Result<SafetyRecord>> Handle(SendRiskLevelAlertCommand command, CancellationToken cancellationToken);
}