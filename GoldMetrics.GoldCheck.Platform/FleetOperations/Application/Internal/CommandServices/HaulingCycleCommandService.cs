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

public class HaulingCycleCommandService(
    IHaulingCycleRepository haulingCycleRepository,
    IUnitOfWork unitOfWork,
    IStringLocalizer<ErrorMessages> localizer)
    : IHaulingCycleCommandService
{
    public async Task<Result<HaulingCycle>> Handle(StartHaulingCycleCommand command, CancellationToken cancellationToken)
    {
        var cycle = new HaulingCycle(command);
        try
        {
            await haulingCycleRepository.AddAsync(cycle, cancellationToken);
            await unitOfWork.CompleteAsync(cancellationToken);
            return Result<HaulingCycle>.Success(cycle);
        }
        catch (OperationCanceledException)
        {
            return Result<HaulingCycle>.Failure(
                FleetOperationsError.OperationCancelled,
                localizer[nameof(FleetOperationsError.OperationCancelled)]);
        }
        catch (DbUpdateException)
        {
            return Result<HaulingCycle>.Failure(
                FleetOperationsError.DatabaseError,
                localizer[nameof(FleetOperationsError.DatabaseError)]);
        }
        catch (Exception)
        {
            return Result<HaulingCycle>.Failure(
                FleetOperationsError.InternalServerError,
                localizer[nameof(FleetOperationsError.InternalServerError)]);
        }
    }

    public async Task<Result<HaulingCycle>> Handle(LoadMaterialCommand command, CancellationToken cancellationToken)
    {
        var cycle = await haulingCycleRepository.FindByIdAsync(command.Id, cancellationToken);
        if (cycle is null)
            return Result<HaulingCycle>.Failure(
                FleetOperationsError.HaulingCycleNotFound,
                localizer[nameof(FleetOperationsError.HaulingCycleNotFound)]);

        try
        {
            cycle.LoadMaterial(command);
            haulingCycleRepository.Update(cycle);
            await unitOfWork.CompleteAsync(cancellationToken);
            return Result<HaulingCycle>.Success(cycle);
        }
        catch (ArgumentException)
        {
            return Result<HaulingCycle>.Failure(
                FleetOperationsError.InvalidPayload,
                localizer[nameof(FleetOperationsError.InvalidPayload)]);
        }
        catch (OperationCanceledException)
        {
            return Result<HaulingCycle>.Failure(
                FleetOperationsError.OperationCancelled,
                localizer[nameof(FleetOperationsError.OperationCancelled)]);
        }
        catch (DbUpdateException)
        {
            return Result<HaulingCycle>.Failure(
                FleetOperationsError.DatabaseError,
                localizer[nameof(FleetOperationsError.DatabaseError)]);
        }
        catch (Exception)
        {
            return Result<HaulingCycle>.Failure(
                FleetOperationsError.InternalServerError,
                localizer[nameof(FleetOperationsError.InternalServerError)]);
        }
    }

    public async Task<Result<HaulingCycle>> Handle(CompleteHaulingCycleCommand command, CancellationToken cancellationToken)
    {
        var cycle = await haulingCycleRepository.FindByIdAsync(command.Id, cancellationToken);
        if (cycle is null)
            return Result<HaulingCycle>.Failure(
                FleetOperationsError.HaulingCycleNotFound,
                localizer[nameof(FleetOperationsError.HaulingCycleNotFound)]);

        try
        {
            cycle.Complete(command);
            haulingCycleRepository.Update(cycle);
            await unitOfWork.CompleteAsync(cancellationToken);
            return Result<HaulingCycle>.Success(cycle);
        }
        catch (OperationCanceledException)
        {
            return Result<HaulingCycle>.Failure(
                FleetOperationsError.OperationCancelled,
                localizer[nameof(FleetOperationsError.OperationCancelled)]);
        }
        catch (DbUpdateException)
        {
            return Result<HaulingCycle>.Failure(
                FleetOperationsError.DatabaseError,
                localizer[nameof(FleetOperationsError.DatabaseError)]);
        }
        catch (Exception)
        {
            return Result<HaulingCycle>.Failure(
                FleetOperationsError.InternalServerError,
                localizer[nameof(FleetOperationsError.InternalServerError)]);
        }
    }

    public async Task<Result<HaulingCycle>> Handle(UpdateRouteProgressCommand command, CancellationToken cancellationToken)
    {
        var cycle = await haulingCycleRepository.FindByIdAsync(command.Id, cancellationToken);
        if (cycle is null)
            return Result<HaulingCycle>.Failure(
                FleetOperationsError.HaulingCycleNotFound,
                localizer[nameof(FleetOperationsError.HaulingCycleNotFound)]);

        try
        {
            cycle.UpdateRouteProgress(command);
            haulingCycleRepository.Update(cycle);
            await unitOfWork.CompleteAsync(cancellationToken);
            return Result<HaulingCycle>.Success(cycle);
        }
        catch (OperationCanceledException)
        {
            return Result<HaulingCycle>.Failure(
                FleetOperationsError.OperationCancelled,
                localizer[nameof(FleetOperationsError.OperationCancelled)]);
        }
        catch (DbUpdateException)
        {
            return Result<HaulingCycle>.Failure(
                FleetOperationsError.DatabaseError,
                localizer[nameof(FleetOperationsError.DatabaseError)]);
        }
        catch (Exception)
        {
            return Result<HaulingCycle>.Failure(
                FleetOperationsError.InternalServerError,
                localizer[nameof(FleetOperationsError.InternalServerError)]);
        }
    }
}
