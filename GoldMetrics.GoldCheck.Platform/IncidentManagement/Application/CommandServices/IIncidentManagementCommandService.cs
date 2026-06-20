using GoldMetrics.GoldCheck.Platform.IncidentManagement.Domain.Model.Aggregates;
using GoldMetrics.GoldCheck.Platform.IncidentManagement.Domain.Model.Commands;
using GoldMetrics.GoldCheck.Platform.Shared.Application.Model;

namespace GoldMetrics.GoldCheck.Platform.IncidentManagement.Application.CommandServices;

public interface IIncidentManagementCommandService
{
    Task<Result<SafetyRecord>> Handle(DetectDriverFatigueCommand command, CancellationToken cancellationToken);
}