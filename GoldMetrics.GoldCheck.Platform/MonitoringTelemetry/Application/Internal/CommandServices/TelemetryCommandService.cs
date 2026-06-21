using GoldMetrics.GoldCheck.Platform.AssetMaintenance.Interfaces.Acl;
using GoldMetrics.GoldCheck.Platform.MonitoringTelemetry.Application.CommandServices;
using GoldMetrics.GoldCheck.Platform.MonitoringTelemetry.Domain.Exceptions;
using GoldMetrics.GoldCheck.Platform.MonitoringTelemetry.Domain.Model.Aggregates;
using GoldMetrics.GoldCheck.Platform.MonitoringTelemetry.Domain.Model.Commands;
using GoldMetrics.GoldCheck.Platform.MonitoringTelemetry.Domain.Repositories;
using GoldMetrics.GoldCheck.Platform.Shared.Application.Model;
using GoldMetrics.GoldCheck.Platform.Shared.Domain.Repositories;
using GoldMetrics.GoldCheck.Platform.Shared.Resources.Errors;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace GoldMetrics.GoldCheck.Platform.MonitoringTelemetry.Application.Internal.CommandServices;

public class TelemetryCommandService(
    ITelemetryDataRepository repository,
    IUnitOfWork unitOfWork,
    IStringLocalizer<ErrorMessages> localizer,
    IAssetMaintenanceContextFacade assetMaintenanceContextFacade)
    : ITelemetryCommandService
{
    public async Task<Result<TelemetryData>> Handle(ProcessTelemetryDataCommand command, CancellationToken cancellationToken = default)
    {
        var assetExists = await assetMaintenanceContextFacade
            .ValidateMachineryExists(command.AssetId, cancellationToken);
        if (!assetExists)
            return Result<TelemetryData>.Failure(
                MonitoringTelemetryError.AssetNotFound,
                localizer[nameof(MonitoringTelemetryError.AssetNotFound)]);

        var data = new TelemetryData(command);
        try
        {
            await repository.AddAsync(data, cancellationToken);
            await unitOfWork.CompleteAsync(cancellationToken);
            return Result<TelemetryData>.Success(data);
        }
        catch (OperationCanceledException)
        {
            return Result<TelemetryData>.Failure(MonitoringTelemetryError.OperationCancelled, localizer[nameof(MonitoringTelemetryError.OperationCancelled)]);
        }
        catch (DbUpdateException)
        {
            return Result<TelemetryData>.Failure(MonitoringTelemetryError.DatabaseError, localizer[nameof(MonitoringTelemetryError.DatabaseError)]);
        }
        catch (Exception)
        {
            return Result<TelemetryData>.Failure(MonitoringTelemetryError.InternalServerError, localizer[nameof(MonitoringTelemetryError.InternalServerError)]);
        }
    }
    
    public async Task<Result<TelemetryData>> Handle(ValidateTelemetryDataCommand command, CancellationToken cancellationToken = default)
    {
        var data = await repository.FindByTelemetryDataIdAsync(command.TelemetryDataId, cancellationToken);
        if (data is null)
            return Result<TelemetryData>.Failure(MonitoringTelemetryError.TelemetryDataNotFound, localizer[nameof(MonitoringTelemetryError.TelemetryDataNotFound)]);
        try
        {
            data.Validate(command);
            await unitOfWork.CompleteAsync(cancellationToken);
            return Result<TelemetryData>.Success(data);
        }
        catch (OperationCanceledException)
        {
            return Result<TelemetryData>.Failure(MonitoringTelemetryError.OperationCancelled, localizer[nameof(MonitoringTelemetryError.OperationCancelled)]);
        }
        catch (DbUpdateException)
        {
            return Result<TelemetryData>.Failure(MonitoringTelemetryError.DatabaseError, localizer[nameof(MonitoringTelemetryError.DatabaseError)]);
        }
        catch (Exception)
        {
            return Result<TelemetryData>.Failure(MonitoringTelemetryError.InternalServerError, localizer[nameof(MonitoringTelemetryError.InternalServerError)]);
        }
    }
}