using GoldMetrics.GoldCheck.Platform.FleetOperations.Application.CommandServices;
using GoldMetrics.GoldCheck.Platform.FleetOperations.Domain.Model;
using GoldMetrics.GoldCheck.Platform.FleetOperations.Domain.Model.Aggregates;
using GoldMetrics.GoldCheck.Platform.FleetOperations.Domain.Model.Commands;
using GoldMetrics.GoldCheck.Platform.FleetOperations.Domain.Repositories;
using GoldMetrics.GoldCheck.Platform.Shared.Application.Model;
using GoldMetrics.GoldCheck.Platform.Shared.Domain.Repositories;
using GoldMetrics.GoldCheck.Platform.Shared.Resources.Errors;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace GoldMetrics.GoldCheck.Platform.FleetOperations.Application.Internal.CommandServices;

public class VehicleCommandService(
    IVehicleRepository vehicleRepository,
    IUnitOfWork unitOfWork,
    IStringLocalizer<ErrorMessages> localizer)
    : IVehicleCommandService
{
    public async Task<Result<Vehicle>> Handle(AssignVehicleCommand command, CancellationToken cancellationToken)
    {
        var existing = await vehicleRepository.FindByVehicleIdAsync(command.VehicleId, cancellationToken);
        if (existing is not null)
            return Result<Vehicle>.Failure(
                FleetOperationsError.VehicleAlreadyAssigned,
                localizer[nameof(FleetOperationsError.VehicleAlreadyAssigned)]);

        var vehicle = new Vehicle(command);
        try
        {
            await vehicleRepository.AddAsync(vehicle, cancellationToken);
            await unitOfWork.CompleteAsync(cancellationToken);
            return Result<Vehicle>.Success(vehicle);
        }
        catch (OperationCanceledException)
        {
            return Result<Vehicle>.Failure(
                FleetOperationsError.OperationCancelled,
                localizer[nameof(FleetOperationsError.OperationCancelled)]);
        }
        catch (DbUpdateException)
        {
            return Result<Vehicle>.Failure(
                FleetOperationsError.DatabaseError,
                localizer[nameof(FleetOperationsError.DatabaseError)]);
        }
        catch (Exception)
        {
            return Result<Vehicle>.Failure(
                FleetOperationsError.InternalServerError,
                localizer[nameof(FleetOperationsError.InternalServerError)]);
        }
    }
}
