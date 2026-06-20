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

    public class TemperatureCommandService(
        ITemperatureReadingRepository repository,
        IUnitOfWork unitOfWork,
        IStringLocalizer<ErrorMessages> localizer)
        : ITemperatureCommandService
    {
        public async Task<Result<TemperatureReading>> Handle(MonitorEngineTemperatureCommand command, CancellationToken cancellationToken = default)
        {
            var reading = new TemperatureReading(command);
            try
            {
                await repository.AddAsync(reading, cancellationToken);
                await unitOfWork.CompleteAsync(cancellationToken);
                return Result<TemperatureReading>.Success(reading);
            }
            catch (OperationCanceledException)
            {
                return Result<TemperatureReading>.Failure(MonitoringTelemetryError.OperationCancelled, localizer[nameof(MonitoringTelemetryError.OperationCancelled)]);
            }
            catch (DbUpdateException)
            {
                return Result<TemperatureReading>.Failure(MonitoringTelemetryError.DatabaseError, localizer[nameof(MonitoringTelemetryError.DatabaseError)]);
            }
            catch (Exception)
            {
                return Result<TemperatureReading>.Failure(MonitoringTelemetryError.InternalServerError, localizer[nameof(MonitoringTelemetryError.InternalServerError)]);
            }
        }

        private async Task<TemperatureReading?> FindLatestByAsset(string assetId, CancellationToken ct)
        {
            var readings = await repository.FindByAssetIdAsync(assetId, ct);
            return readings.MaxBy(r => r.CreatedAt);
        }
        public async Task<Result<TemperatureReading>> Handle(AnalyseExhaustTemperatureCommand command, CancellationToken cancellationToken = default)
        {
            var reading = await FindLatestByAsset(command.AssetId, cancellationToken);
            if (reading is null)
                return Result<TemperatureReading>.Failure(MonitoringTelemetryError.TemperatureReadingNotFound, localizer[nameof(MonitoringTelemetryError.TemperatureReadingNotFound)]);
            try
            {
                reading.AnalyseExhaust(command);
                await unitOfWork.CompleteAsync(cancellationToken);
                return Result<TemperatureReading>.Success(reading);
            }
            catch (ArgumentException)
            {
                return Result<TemperatureReading>.Failure(MonitoringTelemetryError.InvalidTemperature, localizer[nameof(MonitoringTelemetryError.InvalidTemperature)]);
            }
            catch (OperationCanceledException)
            {
                return Result<TemperatureReading>.Failure(MonitoringTelemetryError.OperationCancelled, localizer[nameof(MonitoringTelemetryError.OperationCancelled)]);
            }
            catch (DbUpdateException)
            {
                return Result<TemperatureReading>.Failure(MonitoringTelemetryError.DatabaseError, localizer[nameof(MonitoringTelemetryError.DatabaseError)]);
            }
            catch (Exception)
            {
                return Result<TemperatureReading>.Failure(MonitoringTelemetryError.InternalServerError, localizer[nameof(MonitoringTelemetryError.InternalServerError)]);
            }
        }
        public async Task<Result<TemperatureReading>> Handle(AnalyseExhaustTemperatureLimitPerCylinderCommand command, CancellationToken cancellationToken = default)
        {
            var reading = await FindLatestByAsset(command.AssetId, cancellationToken);
            if (reading is null)
                return Result<TemperatureReading>.Failure(MonitoringTelemetryError.TemperatureReadingNotFound, localizer[nameof(MonitoringTelemetryError.TemperatureReadingNotFound)]);
            try
            {
                reading.AnalyseExhaustLimitPerCylinder(command);
                await unitOfWork.CompleteAsync(cancellationToken);
                return Result<TemperatureReading>.Success(reading);
            }
            catch (ArgumentException)
            {
                return Result<TemperatureReading>.Failure(MonitoringTelemetryError.InvalidTemperature, localizer[nameof(MonitoringTelemetryError.InvalidTemperature)]);
            }
            catch (OperationCanceledException)
            {
                return Result<TemperatureReading>.Failure(MonitoringTelemetryError.OperationCancelled, localizer[nameof(MonitoringTelemetryError.OperationCancelled)]);
            }
            catch (DbUpdateException)
            {
                return Result<TemperatureReading>.Failure(MonitoringTelemetryError.DatabaseError, localizer[nameof(MonitoringTelemetryError.DatabaseError)]);
            }
            catch (Exception)
            {
                return Result<TemperatureReading>.Failure(MonitoringTelemetryError.InternalServerError, localizer[nameof(MonitoringTelemetryError.InternalServerError)]);
            }
        }
        public async Task<Result<TemperatureReading>> Handle(AnalyseEngineRefrigerantTemperatureCommand command, CancellationToken cancellationToken = default)
        {
            var reading = await FindLatestByAsset(command.AssetId, cancellationToken);
            if (reading is null)
                return Result<TemperatureReading>.Failure(MonitoringTelemetryError.TemperatureReadingNotFound, localizer[nameof(MonitoringTelemetryError.TemperatureReadingNotFound)]);
            try
            {
                reading.AnalyseRefrigerant(command);
                await unitOfWork.CompleteAsync(cancellationToken);
                return Result<TemperatureReading>.Success(reading);
            }
            catch (ArgumentException)
            {
                return Result<TemperatureReading>.Failure(MonitoringTelemetryError.InvalidTemperature, localizer[nameof(MonitoringTelemetryError.InvalidTemperature)]);
            }
            catch (OperationCanceledException)
            {
                return Result<TemperatureReading>.Failure(MonitoringTelemetryError.OperationCancelled, localizer[nameof(MonitoringTelemetryError.OperationCancelled)]);
            }
            catch (DbUpdateException)
            {
                return Result<TemperatureReading>.Failure(MonitoringTelemetryError.DatabaseError, localizer[nameof(MonitoringTelemetryError.DatabaseError)]);
            }
            catch (Exception)
            {
                return Result<TemperatureReading>.Failure(MonitoringTelemetryError.InternalServerError, localizer[nameof(MonitoringTelemetryError.InternalServerError)]);
            }
        }
    }