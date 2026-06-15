using GoldMetrics.GoldCheck.Platform.MaterialOperations.Application.CommandServices;
using GoldMetrics.GoldCheck.Platform.MaterialOperations.Domain.Model;
using GoldMetrics.GoldCheck.Platform.MaterialOperations.Domain.Model.Aggregates;
using GoldMetrics.GoldCheck.Platform.MaterialOperations.Domain.Model.Commands;
using GoldMetrics.GoldCheck.Platform.MaterialOperations.Domain.Repositories;
using GoldMetrics.GoldCheck.Platform.Shared.Application.Model;
using GoldMetrics.GoldCheck.Platform.Shared.Domain.Repositories;
using GoldMetrics.GoldCheck.Platform.Shared.Resources.Errors;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace GoldMetrics.GoldCheck.Platform.MaterialOperations.Application.Internal.CommandServices;

public class MaterialCommandService(
    IMaterialRepository materialRepository,
    IUnitOfWork unitOfWork,
    IStringLocalizer<ErrorMessages> localizer)
    : IMaterialCommandService
{
    public async Task<Result<Material>> Handle(IdentifyMineralTypeCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var material = new Material(command);
            await materialRepository.AddAsync(material, cancellationToken);
            await unitOfWork.CompleteAsync(cancellationToken);
            return Result<Material>.Success(material);
        }
        catch (ArgumentException)
        {
            return Result<Material>.Failure(
                MaterialOperationsError.InvalidMineralType,
                localizer[nameof(MaterialOperationsError.InvalidMineralType)]);
        }
        catch (OperationCanceledException)
        {
            return Result<Material>.Failure(
                MaterialOperationsError.OperationCancelled,
                localizer[nameof(MaterialOperationsError.OperationCancelled)]);
        }
        catch (DbUpdateException)
        {
            return Result<Material>.Failure(
                MaterialOperationsError.DatabaseError,
                localizer[nameof(MaterialOperationsError.DatabaseError)]);
        }
        catch (Exception)
        {
            return Result<Material>.Failure(
                MaterialOperationsError.InternalServerError,
                localizer[nameof(MaterialOperationsError.InternalServerError)]);
        }
    }
}
