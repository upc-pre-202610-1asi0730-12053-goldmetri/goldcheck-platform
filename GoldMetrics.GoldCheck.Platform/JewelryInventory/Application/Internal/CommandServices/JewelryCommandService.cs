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

public class JewelryCommandService(
    IJewelryRepository jewelryRepository,
    IJewelryMaterialRepository materialRepository,
    IUnitOfWork unitOfWork,
    IStringLocalizer<ErrorMessages> localizer)
    : IJewelryCommandService
{
    public async Task<Result<Jewelry>> Handle(
        SignCertificateCommand command,
        CancellationToken cancellationToken = default)
    {
        var material = await materialRepository.FindByCertificateIdAsync(
            command.CertificateId, cancellationToken);

        if (material is null)
            return Result<Jewelry>.Failure(
                JewelryInventoryError.CertificateNotFound,
                localizer[nameof(JewelryInventoryError.CertificateNotFound)]);

        var existing = await jewelryRepository.FindByCertificateIdAsync(
            command.CertificateId, cancellationToken);

        if (existing is not null)
            return Result<Jewelry>.Failure(
                JewelryInventoryError.MaterialAlreadyCertified,
                localizer[nameof(JewelryInventoryError.MaterialAlreadyCertified)]);

        var jewelry = new Jewelry(command, material.MaterialId.Value);
        try
        {
            await jewelryRepository.AddAsync(jewelry, cancellationToken);
            await unitOfWork.CompleteAsync(cancellationToken);
            return Result<Jewelry>.Success(jewelry);
        }
        catch (OperationCanceledException)
        {
            return Result<Jewelry>.Failure(
                JewelryInventoryError.OperationCancelled,
                localizer[nameof(JewelryInventoryError.OperationCancelled)]);
        }
        catch (DbUpdateException)
        {
            return Result<Jewelry>.Failure(
                JewelryInventoryError.DatabaseError,
                localizer[nameof(JewelryInventoryError.DatabaseError)]);
        }
        catch (Exception)
        {
            return Result<Jewelry>.Failure(
                JewelryInventoryError.InternalServerError,
                localizer[nameof(JewelryInventoryError.InternalServerError)]);
        }
    }
}
