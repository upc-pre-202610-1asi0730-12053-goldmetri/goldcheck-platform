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
       IStringLocalizer<ErrorMessages> localizer)
       : IPressureCommandService
   {
       public async Task<Result<PressureReading>> Handle(MonitorPressureCommand command, CancellationToken cancellationToken = default)
       {
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
   }