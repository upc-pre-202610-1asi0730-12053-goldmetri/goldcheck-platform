using GoldMetrics.GoldCheck.Platform.IncidentManagement.Application.CommandServices;
using GoldMetrics.GoldCheck.Platform.IncidentManagement.Domain.Model.Aggregates;
using GoldMetrics.GoldCheck.Platform.IncidentManagement.Domain.Model.Commands;
using GoldMetrics.GoldCheck.Platform.IncidentManagement.Domain.Model.Errors;
using GoldMetrics.GoldCheck.Platform.IncidentManagement.Domain.Repositories;
using GoldMetrics.GoldCheck.Platform.Shared.Application.Model;
using GoldMetrics.GoldCheck.Platform.Shared.Domain.Repositories;
using GoldMetrics.GoldCheck.Platform.Shared.Resources.Errors;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace GoldMetrics.GoldCheck.Platform.IncidentManagement.Application.Internal.CommandServices;

public class IncidentManagementCommandService(
    ISafetyRecordRepository safetyRecordRepository,
    IUnitOfWork unitOfWork,
    IStringLocalizer<ErrorMessages> localizer)
    : IIncidentManagementCommandService
{
    private async Task<SafetyRecord?> FindByIncidentId(int id, CancellationToken ct)
        => await safetyRecordRepository.FindByIdAsync(id, ct);

    private Result<SafetyRecord> Cancelled() =>
        Result<SafetyRecord>.Failure(IncidentManagementError.OperationCancelled, localizer[nameof(IncidentManagementError.OperationCancelled)]);
    private Result<SafetyRecord> DbError() =>
        Result<SafetyRecord>.Failure(IncidentManagementError.DatabaseError, localizer[nameof(IncidentManagementError.DatabaseError)]);
    private Result<SafetyRecord> ServerError() =>
        Result<SafetyRecord>.Failure(IncidentManagementError.InternalServerError, localizer[nameof(IncidentManagementError.InternalServerError)]);
    private Result<SafetyRecord> NotFound() =>
        Result<SafetyRecord>.Failure(IncidentManagementError.IncidentNotFound, localizer[nameof(IncidentManagementError.IncidentNotFound)]);

    public async Task<Result<SafetyRecord>> Handle(DetectDriverFatigueCommand command, CancellationToken cancellationToken)
    {
        var record = new SafetyRecord(command);
        try
        {
            await safetyRecordRepository.AddAsync(record, cancellationToken);
            await unitOfWork.CompleteAsync(cancellationToken);
            return Result<SafetyRecord>.Success(record);
        }
        catch (ArgumentException)
        {
            return Result<SafetyRecord>.Failure(IncidentManagementError.InvalidIncidentType, localizer[nameof(IncidentManagementError.InvalidIncidentType)]);
        }
        catch (OperationCanceledException) { return Cancelled(); }
        catch (DbUpdateException) { return DbError(); }
        catch (Exception) { return ServerError(); }
    }
    
    public async Task<Result<SafetyRecord>> Handle(EscalateRiskLevelCommand command, CancellationToken cancellationToken)
    {
        var record = await FindByIncidentId(command.Id, cancellationToken);
        if (record is null) return NotFound();
        try
        {
            record.EscalateRiskLevel(command);
            safetyRecordRepository.Update(record);
            await unitOfWork.CompleteAsync(cancellationToken);
            return Result<SafetyRecord>.Success(record);
        }
        catch (ArgumentException)
        {
            return Result<SafetyRecord>.Failure(IncidentManagementError.InvalidRiskLevel, localizer[nameof(IncidentManagementError.InvalidRiskLevel)]);
        }
        catch (OperationCanceledException) { return Cancelled(); }
        catch (DbUpdateException) { return DbError(); }
        catch (Exception) { return ServerError(); }
    }
    
    public async Task<Result<SafetyRecord>> Handle(EvaluateSafetyRiskCommand command, CancellationToken cancellationToken)
    {
        var record = await FindByIncidentId(command.Id, cancellationToken);
        if (record is null) return NotFound();
        try
        {
            record.EvaluateSafetyRisk(command);
            safetyRecordRepository.Update(record);
            await unitOfWork.CompleteAsync(cancellationToken);
            return Result<SafetyRecord>.Success(record);
        }
        catch (OperationCanceledException) { return Cancelled(); }
        catch (DbUpdateException) { return DbError(); }
        catch (Exception) { return ServerError(); }
    }
    
    public async Task<Result<SafetyRecord>> Handle(SendRiskLevelAlertCommand command, CancellationToken cancellationToken)
    {
        var record = await FindByIncidentId(command.Id, cancellationToken);
        if (record is null) return NotFound();
        try
        {
            record.SendRiskLevelAlert(command);
            safetyRecordRepository.Update(record);
            await unitOfWork.CompleteAsync(cancellationToken);
            return Result<SafetyRecord>.Success(record);
        }
        catch (OperationCanceledException) { return Cancelled(); }
        catch (DbUpdateException) { return DbError(); }
        catch (Exception) { return ServerError(); }
    }
    
    public async Task<Result<SafetyRecord>> Handle(DetectSmokeCommand command, CancellationToken cancellationToken)
    {
        var record = new SafetyRecord(command);
        try
        {
            await safetyRecordRepository.AddAsync(record, cancellationToken);
            await unitOfWork.CompleteAsync(cancellationToken);
            return Result<SafetyRecord>.Success(record);
        }
        catch (OperationCanceledException) { return Cancelled(); }
        catch (DbUpdateException) { return DbError(); }
        catch (Exception) { return ServerError(); }
    }
}