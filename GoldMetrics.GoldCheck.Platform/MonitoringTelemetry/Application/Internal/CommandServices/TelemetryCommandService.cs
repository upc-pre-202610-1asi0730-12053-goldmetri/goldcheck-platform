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
    IStringLocalizer<ErrorMessages> localizer)
    : ITelemetryCommandService
{
    public async Task<Result<TelemetryData>> Handle(ProcessTelemetryDataCommand command, CancellationToken cancellationToken = default)
    {
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
}