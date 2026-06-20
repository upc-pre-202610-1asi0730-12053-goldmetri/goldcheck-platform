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

public class GNSSCommandService(
    IGNSSStatusRepository repository,
    IUnitOfWork unitOfWork,
    IStringLocalizer<ErrorMessages> localizer)
    : IGNSSCommandService
{
    public async Task<Result<GNSSStatus>> Handle(MonitorGNSSStatusCommand command, CancellationToken cancellationToken = default)
    {
        var status = new GNSSStatus(command);
        try
        {
            await repository.AddAsync(status, cancellationToken);
            await unitOfWork.CompleteAsync(cancellationToken);
            return Result<GNSSStatus>.Success(status);
        }
        catch (OperationCanceledException)
        {
            return Result<GNSSStatus>.Failure(MonitoringTelemetryError.OperationCancelled, localizer[nameof(MonitoringTelemetryError.OperationCancelled)]);
        }
        catch (DbUpdateException)
        {
            return Result<GNSSStatus>.Failure(MonitoringTelemetryError.DatabaseError, localizer[nameof(MonitoringTelemetryError.DatabaseError)]);
        }
        catch (Exception)
        {
            return Result<GNSSStatus>.Failure(MonitoringTelemetryError.InternalServerError, localizer[nameof(MonitoringTelemetryError.InternalServerError)]);
        }
    }
    
    public async Task<Result<GNSSStatus>> Handle(DetectGNSSAnomalyCommand command, CancellationToken cancellationToken = default)
    {
        var statuses = await repository.FindByAssetIdAsync(command.AssetId, cancellationToken);
        var status = statuses.MaxBy(s => s.CreatedAt);
        if (status is null)
            return Result<GNSSStatus>.Failure(MonitoringTelemetryError.GNSSStatusNotFound, localizer[nameof(MonitoringTelemetryError.GNSSStatusNotFound)]);
        try
        {
            status.DetectAnomaly(command);
            await unitOfWork.CompleteAsync(cancellationToken);
            return Result<GNSSStatus>.Success(status);
        }
        catch (OperationCanceledException)
        {
            return Result<GNSSStatus>.Failure(MonitoringTelemetryError.OperationCancelled, localizer[nameof(MonitoringTelemetryError.OperationCancelled)]);
        }
        catch (DbUpdateException)
        {
            return Result<GNSSStatus>.Failure(MonitoringTelemetryError.DatabaseError, localizer[nameof(MonitoringTelemetryError.DatabaseError)]);
        }
        catch (Exception)
        {
            return Result<GNSSStatus>.Failure(MonitoringTelemetryError.InternalServerError, localizer[nameof(MonitoringTelemetryError.InternalServerError)]);
        }
    }
}