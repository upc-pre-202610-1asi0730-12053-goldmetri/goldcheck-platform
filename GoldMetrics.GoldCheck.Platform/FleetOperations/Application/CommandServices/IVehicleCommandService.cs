using GoldMetrics.GoldCheck.Platform.FleetOperations.Domain.Model.Aggregates;
using GoldMetrics.GoldCheck.Platform.FleetOperations.Domain.Model.Commands;
using GoldMetrics.GoldCheck.Platform.Shared.Application.Model;

namespace GoldMetrics.GoldCheck.Platform.FleetOperations.Application.CommandServices;

public interface IVehicleCommandService
{
    Task<Result<Vehicle>> Handle(AssignVehicleCommand command, CancellationToken cancellationToken);
    Task<Result<Vehicle>> Handle(StartEngineCommand command, CancellationToken cancellationToken);
}
