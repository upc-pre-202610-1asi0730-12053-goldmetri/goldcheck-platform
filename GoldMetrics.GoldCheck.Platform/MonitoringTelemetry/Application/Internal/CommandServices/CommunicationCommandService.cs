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

   public class CommunicationCommandService(
       ICommunicationChannelRepository repository,
       IUnitOfWork unitOfWork,
       IStringLocalizer<ErrorMessages> localizer)
       : ICommunicationCommandService
   {
       public async Task<Result<CommunicationChannel>> Handle(MonitorCommunicationChannelCommand command, CancellationToken cancellationToken = default)
       {
           var channel = new CommunicationChannel(command);
           try
           {
               await repository.AddAsync(channel, cancellationToken);
               await unitOfWork.CompleteAsync(cancellationToken);
               return Result<CommunicationChannel>.Success(channel);
           }
           catch (OperationCanceledException)
           {
               return Result<CommunicationChannel>.Failure(MonitoringTelemetryError.OperationCancelled, localizer[nameof(MonitoringTelemetryError.OperationCancelled)]);
           }
           catch (DbUpdateException)
           {
               return Result<CommunicationChannel>.Failure(MonitoringTelemetryError.DatabaseError, localizer[nameof(MonitoringTelemetryError.DatabaseError)]);
           }
           catch (Exception)
           {
               return Result<CommunicationChannel>.Failure(MonitoringTelemetryError.InternalServerError, localizer[nameof(MonitoringTelemetryError.InternalServerError)]);
           }
       }
       
       public async Task<Result<CommunicationChannel>> Handle(AnalyseCommunicationCommand command, CancellationToken cancellationToken = default)
       {
           var channels = await repository.FindByAssetIdAsync(command.AssetId, cancellationToken);
           var channel = channels.MaxBy(c => c.CreatedAt);
           if (channel is null)
               return Result<CommunicationChannel>.Failure(MonitoringTelemetryError.CommunicationChannelNotFound, localizer[nameof(MonitoringTelemetryError.CommunicationChannelNotFound)]);
           try
           {
               channel.AnalyseCommunication(command);
               await unitOfWork.CompleteAsync(cancellationToken);
               return Result<CommunicationChannel>.Success(channel);
           }
           catch (ArgumentException)
           {
               return Result<CommunicationChannel>.Failure(MonitoringTelemetryError.InvalidProtocol, localizer[nameof(MonitoringTelemetryError.InvalidProtocol)]);
           }
           catch (OperationCanceledException)
           {
               return Result<CommunicationChannel>.Failure(MonitoringTelemetryError.OperationCancelled, localizer[nameof(MonitoringTelemetryError.OperationCancelled)]);
           }
           catch (DbUpdateException)
           {
               return Result<CommunicationChannel>.Failure(MonitoringTelemetryError.DatabaseError, localizer[nameof(MonitoringTelemetryError.DatabaseError)]);
           }
           catch (Exception)
           {
               return Result<CommunicationChannel>.Failure(MonitoringTelemetryError.InternalServerError, localizer[nameof(MonitoringTelemetryError.InternalServerError)]);
           }
       }
       
       public async Task<Result<CommunicationChannel>> Handle(DetectCommunicationAnomalyCommand command, CancellationToken cancellationToken = default)
       {
           var channels = await repository.FindByAssetIdAsync(command.AssetId, cancellationToken);
           var channel = channels.MaxBy(c => c.CreatedAt);
           if (channel is null)
               return Result<CommunicationChannel>.Failure(MonitoringTelemetryError.CommunicationChannelNotFound, localizer[nameof(MonitoringTelemetryError.CommunicationChannelNotFound)]);
           try
           {
               channel.DetectAnomaly(command);
               channel.LogAnomaly(new LogCommunicationAnomalyCommand(command.AssetId, command.Protocol, command.AnomalyDescription));
               await unitOfWork.CompleteAsync(cancellationToken);
               return Result<CommunicationChannel>.Success(channel);
           }
           catch (ArgumentException)
           {
               return Result<CommunicationChannel>.Failure(MonitoringTelemetryError.InvalidProtocol, localizer[nameof(MonitoringTelemetryError.InvalidProtocol)]);
           }
           catch (OperationCanceledException)
           {
               return Result<CommunicationChannel>.Failure(MonitoringTelemetryError.OperationCancelled, localizer[nameof(MonitoringTelemetryError.OperationCancelled)]);
           }
           catch (DbUpdateException)
           {
               return Result<CommunicationChannel>.Failure(MonitoringTelemetryError.DatabaseError, localizer[nameof(MonitoringTelemetryError.DatabaseError)]);
           }
           catch (Exception)
           {
               return Result<CommunicationChannel>.Failure(MonitoringTelemetryError.InternalServerError, localizer[nameof(MonitoringTelemetryError.InternalServerError)]);
           }
       }
   }