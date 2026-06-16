using GoldMetrics.GoldCheck.Platform.JewelryInventory.Application.CommandServices;
using GoldMetrics.GoldCheck.Platform.JewelryInventory.Domain.Exceptions;
using GoldMetrics.GoldCheck.Platform.JewelryInventory.Domain.Model.Aggregates;
using GoldMetrics.GoldCheck.Platform.JewelryInventory.Domain.Model.Commands;
using GoldMetrics.GoldCheck.Platform.JewelryInventory.Domain.Repositories;
using GoldMetrics.GoldCheck.Platform.Shared.Application.Model;
using GoldMetrics.GoldCheck.Platform.Shared.Domain.Repositories;
using GoldMetrics.GoldCheck.Platform.Shared.Resources.Errors;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace GoldMetrics.GoldCheck.Platform.JewelryInventory.Application.Internal.CommandServices;

public class JewelryMaterialCommandService(
    IJewelryMaterialRepository materialRepository,
    IUnitOfWork unitOfWork,
    IStringLocalizer<ErrorMessages> localizer)
    : IJewelryMaterialCommandService
{
    // ── RegisterNonCertifiedMaterial ──────────────────────────────────────────

    public async Task<Result<JewelryMaterial>> Handle(
        RegisterNonCertifiedMaterialCommand command,
        CancellationToken cancellationToken = default)
    {
        var existing = await materialRepository.FindByMaterialIdAsync(
            command.MaterialId, cancellationToken);

        if (existing is not null)
            return Result<JewelryMaterial>.Failure(
                JewelryInventoryError.MaterialAlreadyCertified,
                localizer[nameof(JewelryInventoryError.MaterialAlreadyCertified)]);

        var material = new JewelryMaterial(command);
        try
        {
            await materialRepository.AddAsync(material, cancellationToken);
            await unitOfWork.CompleteAsync(cancellationToken);
            return Result<JewelryMaterial>.Success(material);
        }
        catch (OperationCanceledException)
        {
            return Result<JewelryMaterial>.Failure(
                JewelryInventoryError.OperationCancelled,
                localizer[nameof(JewelryInventoryError.OperationCancelled)]);
        }
        catch (DbUpdateException)
        {
            return Result<JewelryMaterial>.Failure(
                JewelryInventoryError.DatabaseError,
                localizer[nameof(JewelryInventoryError.DatabaseError)]);
        }
        catch (Exception)
        {
            return Result<JewelryMaterial>.Failure(
                JewelryInventoryError.InternalServerError,
                localizer[nameof(JewelryInventoryError.InternalServerError)]);
        }
    }
}
