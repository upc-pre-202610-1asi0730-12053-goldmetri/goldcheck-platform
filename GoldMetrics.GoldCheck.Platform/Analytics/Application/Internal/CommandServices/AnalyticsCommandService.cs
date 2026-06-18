using GoldMetrics.GoldCheck.Platform.Analytics.Application.CommandServices;
using GoldMetrics.GoldCheck.Platform.Analytics.Domain.Model.Aggregates;
using GoldMetrics.GoldCheck.Platform.Analytics.Domain.Model.Commands;
using GoldMetrics.GoldCheck.Platform.Analytics.Domain.Model.Errors;
using GoldMetrics.GoldCheck.Platform.Analytics.Domain.Repositories;
using GoldMetrics.GoldCheck.Platform.Shared.Application.Model;
using GoldMetrics.GoldCheck.Platform.Shared.Domain.Repositories;
using GoldMetrics.GoldCheck.Platform.Shared.Resources.Errors;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace GoldMetrics.GoldCheck.Platform.Analytics.Application.Internal.CommandServices;

public class AnalyticsCommandService(
    IMaterialRepository materialRepository,
    IUnitOfWork unitOfWork,
    IStringLocalizer<ErrorMessages> localizer)
    : IAnalyticsCommandService
{
    public async Task<Result<Material>> Handle(ViewRouteProgressCommand command, CancellationToken cancellationToken)
    {
        var material = new Material(command);
        try
        {
            await materialRepository.AddAsync(material, cancellationToken);
            await unitOfWork.CompleteAsync(cancellationToken);
            return Result<Material>.Success(material);
        }
        catch (OperationCanceledException)
        {
            return Result<Material>.Failure(
                AnalyticsError.OperationCancelled,
                localizer[nameof(AnalyticsError.OperationCancelled)]);
        }
        catch (DbUpdateException)
        {
            return Result<Material>.Failure(
                AnalyticsError.DatabaseError,
                localizer[nameof(AnalyticsError.DatabaseError)]);
        }
        catch (Exception)
        {
            return Result<Material>.Failure(
                AnalyticsError.InternalServerError,
                localizer[nameof(AnalyticsError.InternalServerError)]);
        }
    }

    public async Task<Result<Material>> Handle(ViewProductionDashboardCommand command, CancellationToken cancellationToken)
    {
        var material = await materialRepository.FindBySupervisorIdAsync(command.SupervisorId, cancellationToken);
        var target = material.MaxBy(m => m.CreatedAt);
        if (target is null)
            return Result<Material>.Failure(
                AnalyticsError.MaterialNotFound,
                localizer[nameof(AnalyticsError.MaterialNotFound)]);
        try
        {
            target.ViewProductionDashboard(command);
            materialRepository.Update(target);
            await unitOfWork.CompleteAsync(cancellationToken);
            return Result<Material>.Success(target);
        }
        catch (OperationCanceledException)
        {
            return Result<Material>.Failure(
                AnalyticsError.OperationCancelled,
                localizer[nameof(AnalyticsError.OperationCancelled)]);
        }
        catch (DbUpdateException)
        {
            return Result<Material>.Failure(
                AnalyticsError.DatabaseError,
                localizer[nameof(AnalyticsError.DatabaseError)]);
        }
        catch (Exception)
        {
            return Result<Material>.Failure(
                AnalyticsError.InternalServerError,
                localizer[nameof(AnalyticsError.InternalServerError)]);
        }
    }

    public async Task<Result<Material>> Handle(RequestProductionDataCommand command, CancellationToken cancellationToken)
    {
        var materials = await materialRepository.FindBySupervisorIdAsync(command.SupervisorId, cancellationToken);
        var target = materials.MaxBy(m => m.CreatedAt);
        if (target is null)
            return Result<Material>.Failure(
                AnalyticsError.MaterialNotFound,
                localizer[nameof(AnalyticsError.MaterialNotFound)]);
        try
        {
            target.RequestProductionData(command);
            materialRepository.Update(target);
            await unitOfWork.CompleteAsync(cancellationToken);
            return Result<Material>.Success(target);
        }
        catch (ArgumentException)
        {
            return Result<Material>.Failure(
                AnalyticsError.InvalidProductionPeriod,
                localizer[nameof(AnalyticsError.InvalidProductionPeriod)]);
        }
        catch (OperationCanceledException)
        {
            return Result<Material>.Failure(
                AnalyticsError.OperationCancelled,
                localizer[nameof(AnalyticsError.OperationCancelled)]);
        }
        catch (DbUpdateException)
        {
            return Result<Material>.Failure(
                AnalyticsError.DatabaseError,
                localizer[nameof(AnalyticsError.DatabaseError)]);
        }
        catch (Exception)
        {
            return Result<Material>.Failure(
                AnalyticsError.InternalServerError,
                localizer[nameof(AnalyticsError.InternalServerError)]);
        }
    }

    public async Task<Result<Material>> Handle(LoadProductionDataCommand command, CancellationToken cancellationToken)
    {
        var material = await materialRepository.FindByMaterialIdAsync(command.MaterialId, cancellationToken);
        if (material is null)
            return Result<Material>.Failure(
                AnalyticsError.MaterialNotFound,
                localizer[nameof(AnalyticsError.MaterialNotFound)]);
        try
        {
            material.LoadProductionData(command);
            materialRepository.Update(material);
            await unitOfWork.CompleteAsync(cancellationToken);
            return Result<Material>.Success(material);
        }
        catch (OperationCanceledException)
        {
            return Result<Material>.Failure(
                AnalyticsError.OperationCancelled,
                localizer[nameof(AnalyticsError.OperationCancelled)]);
        }
        catch (DbUpdateException)
        {
            return Result<Material>.Failure(
                AnalyticsError.DatabaseError,
                localizer[nameof(AnalyticsError.DatabaseError)]);
        }
        catch (Exception)
        {
            return Result<Material>.Failure(
                AnalyticsError.InternalServerError,
                localizer[nameof(AnalyticsError.InternalServerError)]);
        }
    }
}
