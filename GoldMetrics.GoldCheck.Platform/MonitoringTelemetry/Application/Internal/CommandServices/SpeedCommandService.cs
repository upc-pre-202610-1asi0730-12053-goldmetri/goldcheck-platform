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

public class SpeedCommandService(
   ISpeedReadingRepository repository,
   IUnitOfWork unitOfWork,
   IStringLocalizer<ErrorMessages> localizer)
   : ISpeedCommandService
{
   public async Task<Result<SpeedReading>> Handle(MonitorSpeedStatusCommand command, CancellationToken cancellationToken = default)
   {
       var reading = new SpeedReading(command);
       try
       {
           await repository.AddAsync(reading, cancellationToken);
           await unitOfWork.CompleteAsync(cancellationToken);
           return Result<SpeedReading>.Success(reading);
       }
       catch (OperationCanceledException)
       {
           return Result<SpeedReading>.Failure(MonitoringTelemetryError.OperationCancelled, localizer[nameof(MonitoringTelemetryError.OperationCancelled)]);
       }
       catch (DbUpdateException)
       {
           return Result<SpeedReading>.Failure(MonitoringTelemetryError.DatabaseError, localizer[nameof(MonitoringTelemetryError.DatabaseError)]);
       }
       catch (Exception)
       {
           return Result<SpeedReading>.Failure(MonitoringTelemetryError.InternalServerError, localizer[nameof(MonitoringTelemetryError.InternalServerError)]);
       }
   }
   
   public async Task<Result<SpeedReading>> Handle(DetectSpeedExcessCommand command, CancellationToken cancellationToken = default)
   {
       var readings = await repository.FindByAssetIdAsync(command.AssetId, cancellationToken);
       var reading = readings.MaxBy(r => r.CreatedAt);
       if (reading is null)
           return Result<SpeedReading>.Failure(MonitoringTelemetryError.SpeedReadingNotFound, localizer[nameof(MonitoringTelemetryError.SpeedReadingNotFound)]);
       try
       {
           reading.DetectExcess(command);
           reading.LogExcess(new LogSpeedExcessCommand(command.AssetId, command.SpeedKmPerHour));
           await unitOfWork.CompleteAsync(cancellationToken);
           return Result<SpeedReading>.Success(reading);
       }
       catch (ArgumentException)
       {
           return Result<SpeedReading>.Failure(MonitoringTelemetryError.InvalidSpeed, localizer[nameof(MonitoringTelemetryError.InvalidSpeed)]);
       }
       catch (OperationCanceledException)
       {
           return Result<SpeedReading>.Failure(MonitoringTelemetryError.OperationCancelled, localizer[nameof(MonitoringTelemetryError.OperationCancelled)]);
       }
       catch (DbUpdateException)
       {
           return Result<SpeedReading>.Failure(MonitoringTelemetryError.DatabaseError, localizer[nameof(MonitoringTelemetryError.DatabaseError)]);
       }
       catch (Exception)
       {
           return Result<SpeedReading>.Failure(MonitoringTelemetryError.InternalServerError, localizer[nameof(MonitoringTelemetryError.InternalServerError)]);
       }
   }
}