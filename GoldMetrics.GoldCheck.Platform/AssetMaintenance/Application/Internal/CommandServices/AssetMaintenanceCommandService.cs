using GoldMetrics.GoldCheck.Platform.AssetMaintenance.Application.CommandServices;
    using GoldMetrics.GoldCheck.Platform.AssetMaintenance.Domain.Model.Aggregates;
    using GoldMetrics.GoldCheck.Platform.AssetMaintenance.Domain.Model.Commands;
    using GoldMetrics.GoldCheck.Platform.AssetMaintenance.Domain.Model.Errors;
    using GoldMetrics.GoldCheck.Platform.AssetMaintenance.Domain.Repositories;
    using GoldMetrics.GoldCheck.Platform.Shared.Application.Model;
    using GoldMetrics.GoldCheck.Platform.Shared.Domain.Repositories;
    using GoldMetrics.GoldCheck.Platform.Shared.Resources.Errors;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Localization;

    namespace GoldMetrics.GoldCheck.Platform.AssetMaintenance.Application.Internal.CommandServices;

    public class AssetMaintenanceCommandService(
        IMachineryRepository machineryRepository,
        IUnitOfWork unitOfWork,
        IStringLocalizer<ErrorMessages> localizer)
        : IAssetMaintenanceCommandService
    {
        private async Task<Machinery?> FindMachinery(string machineryId, CancellationToken ct)
            => await machineryRepository.FindByMachineryIdAsync(machineryId, ct);

        private Result<Machinery> NotFound() =>
            Result<Machinery>.Failure(AssetMaintenanceError.MachineryNotFound, localizer[nameof(AssetMaintenanceError.MachineryNotFound)]);
        private Result<Machinery> Cancelled() =>
            Result<Machinery>.Failure(AssetMaintenanceError.OperationCancelled, localizer[nameof(AssetMaintenanceError.OperationCancelled)]);
        private Result<Machinery> DbError() =>
            Result<Machinery>.Failure(AssetMaintenanceError.DatabaseError, localizer[nameof(AssetMaintenanceError.DatabaseError)]);
        private Result<Machinery> ServerError() =>
            Result<Machinery>.Failure(AssetMaintenanceError.InternalServerError, localizer[nameof(AssetMaintenanceError.InternalServerError)]);

        public async Task<Result<Machinery>> Handle(RegisterMachineryCommand command, CancellationToken cancellationToken)
        {
            var machinery = new Machinery(command);
            try
            {
                await machineryRepository.AddAsync(machinery, cancellationToken);
                await unitOfWork.CompleteAsync(cancellationToken);
                return Result<Machinery>.Success(machinery);
            }
            catch (OperationCanceledException) { return Cancelled(); }
            catch (DbUpdateException) { return DbError(); }
            catch (Exception) { return ServerError(); }
        }
        
        public async Task<Result<Machinery>> Handle(UpdateMachineryDataCommand command, CancellationToken cancellationToken)
        {
            var machinery = await FindMachinery(command.MachineryId, cancellationToken);
            if (machinery is null) return NotFound();
            try
            {
                machinery.UpdateMachineryData(command);
                machineryRepository.Update(machinery);
                await unitOfWork.CompleteAsync(cancellationToken);
                return Result<Machinery>.Success(machinery);
            }
            catch (ArgumentException)
            {
                return Result<Machinery>.Failure(AssetMaintenanceError.InvalidEngineHours, localizer[nameof(AssetMaintenanceError.InvalidEngineHours)]);
            }
            catch (OperationCanceledException) { return Cancelled(); }
            catch (DbUpdateException) { return DbError(); }
            catch (Exception) { return ServerError(); }
        }
        public async Task<Result<Machinery>> Handle(SchedulePreventiveMaintenanceCommand command, CancellationToken cancellationToken)
        {
            var machinery = await FindMachinery(command.MachineryId, cancellationToken);
            if (machinery is null) return NotFound();
            try
            {
                machinery.SchedulePreventiveMaintenance(command);
                machineryRepository.Update(machinery);
                await unitOfWork.CompleteAsync(cancellationToken);
                return Result<Machinery>.Success(machinery);
            }
            catch (ArgumentException)
            {
                return Result<Machinery>.Failure(AssetMaintenanceError.InvalidEngineHours, localizer[nameof(AssetMaintenanceError.InvalidEngineHours)]);
            }
            catch (OperationCanceledException) { return Cancelled(); }
            catch (DbUpdateException) { return DbError(); }
            catch (Exception) { return ServerError(); }
        }
    }