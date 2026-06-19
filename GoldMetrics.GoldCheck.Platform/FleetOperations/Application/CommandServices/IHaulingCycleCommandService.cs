using GoldMetrics.GoldCheck.Platform.FleetOperations.Domain.Model.Aggregates;
using GoldMetrics.GoldCheck.Platform.FleetOperations.Domain.Model.Commands;
using GoldMetrics.GoldCheck.Platform.Shared.Application.Model;

namespace GoldMetrics.GoldCheck.Platform.FleetOperations.Application.CommandServices;

public interface IHaulingCycleCommandService
{
    Task<Result<HaulingCycle>> Handle(StartHaulingCycleCommand command, CancellationToken cancellationToken);
    Task<Result<HaulingCycle>> Handle(LoadMaterialCommand command, CancellationToken cancellationToken);
}
