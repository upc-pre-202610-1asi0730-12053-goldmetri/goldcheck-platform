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

   public class PressureCommandService(
       IPressureReadingRepository repository,
       IUnitOfWork unitOfWork,
       IStringLocalizer<ErrorMessages> localizer,
       IAssetMaintenanceContextFacade assetMaintenanceContextFacade)
       : IPressureCommandService
   {
       public async Task<Result<PressureReading>> Handle(MonitorPressureCommand command, CancellationToken cancellationToken = default)
       {
           var assetExists = await assetMaintenanceContextFacade
               .ValidateMachineryExists(command.AssetId, cancellationToken);
           if (!assetExists)
               return Result<PressureReading>.Failure(
                   MonitoringTelemetryError.AssetNotFound,
                   localizer[nameof(MonitoringTelemetryError.AssetNotFound)]);

           var reading = new PressureReading(command);
           try
           {
               await repository.AddAsync(reading, cancellationToken);
               await unitOfWork.CompleteAsync(cancellationToken);
               return Result<PressureReading>.Success(reading);
           }
           catch (OperationCanceledException)
           {
               return Result<PressureReading>.Failure(MonitoringTelemetryError.OperationCancelled, localizer[nameof(MonitoringTelemetryError.OperationCancelled)]);
           }
           catch (DbUpdateException)
           {
               return Result<PressureReading>.Failure(MonitoringTelemetryError.DatabaseError, localizer[nameof(MonitoringTelemetryError.DatabaseError)]);
           }
           catch (Exception)
           {
               return Result<PressureReading>.Failure(MonitoringTelemetryError.InternalServerError, localizer[nameof(MonitoringTelemetryError.InternalServerError)]);
           }
       }
       
       public async Task<Result<PressureReading>> Handle(AnalysePressureCommand command, CancellationToken cancellationToken = default)
       {
           var readings = await repository.FindByAssetIdAsync(command.AssetId, cancellationToken);
           var reading = readings.MaxBy(r => r.CreatedAt);
           if (reading is null)
               return Result<PressureReading>.Failure(MonitoringTelemetryError.PressureReadingNotFound, localizer[nameof(MonitoringTelemetryError.PressureReadingNotFound)]);
           try
           {
               reading.AnalysePressure(command);
               await unitOfWork.CompleteAsync(cancellationToken);
               return Result<PressureReading>.Success(reading);
           }
           catch (ArgumentException)
           {
               return Result<PressureReading>.Failure(MonitoringTelemetryError.InvalidPressure, localizer[nameof(MonitoringTelemetryError.InvalidPressure)]);
           }
           catch (OperationCanceledException)
           {
               return Result<PressureReading>.Failure(MonitoringTelemetryError.OperationCancelled, localizer[nameof(MonitoringTelemetryError.OperationCancelled)]);
           }
           catch (DbUpdateException)
           {
               return Result<PressureReading>.Failure(MonitoringTelemetryError.DatabaseError, localizer[nameof(MonitoringTelemetryError.DatabaseError)]);
           }
           catch (Exception)
           {
               return Result<PressureReading>.Failure(MonitoringTelemetryError.InternalServerError, localizer[nameof(MonitoringTelemetryError.InternalServerError)]);
           }
       }
       public async Task<Result<PressureReading>> Handle(DetectPressureAnomalyCommand command, CancellationToken cancellationToken = default)
       {
           var readings = await repository.FindByAssetIdAsync(command.AssetId, cancellationToken);
           var reading = readings.MaxBy(r => r.CreatedAt);
           if (reading is null)
               return Result<PressureReading>.Failure(MonitoringTelemetryError.PressureReadingNotFound, localizer[nameof(MonitoringTelemetryError.PressureReadingNotFound)]);
           try
           {
               reading.DetectAnomaly(command);
               reading.LogAnomaly(new LogPressureAnomalyCommand(command.AssetId, $"Pressure anomaly detected: {command.PressureType} at {command.PressureBar} bar."));
               await unitOfWork.CompleteAsync(cancellationToken);
               return Result<PressureReading>.Success(reading);
           }
           catch (ArgumentException)
           {
               return Result<PressureReading>.Failure(MonitoringTelemetryError.InvalidPressureType, localizer[nameof(MonitoringTelemetryError.InvalidPressureType)]);
           }
           catch (OperationCanceledException)
           {
               return Result<PressureReading>.Failure(MonitoringTelemetryError.OperationCancelled, localizer[nameof(MonitoringTelemetryError.OperationCancelled)]);
           }
           catch (DbUpdateException)
           {
               return Result<PressureReading>.Failure(MonitoringTelemetryError.DatabaseError, localizer[nameof(MonitoringTelemetryError.DatabaseError)]);
           }
           catch (Exception)
           {
               return Result<PressureReading>.Failure(MonitoringTelemetryError.InternalServerError, localizer[nameof(MonitoringTelemetryError.InternalServerError)]);
           }
       }
   }