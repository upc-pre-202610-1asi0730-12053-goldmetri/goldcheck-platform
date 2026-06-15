using GoldMetrics.GoldCheck.Platform.ConsumerTraceability.Application.CommandServices;
using GoldMetrics.GoldCheck.Platform.ConsumerTraceability.Domain.Exceptions;
using GoldMetrics.GoldCheck.Platform.ConsumerTraceability.Domain.Model.Aggregates;
using GoldMetrics.GoldCheck.Platform.ConsumerTraceability.Domain.Model.Commands;
using GoldMetrics.GoldCheck.Platform.ConsumerTraceability.Domain.Repositories;
using GoldMetrics.GoldCheck.Platform.Shared.Application.Model;
using GoldMetrics.GoldCheck.Platform.Shared.Domain.Repositories;
using GoldMetrics.GoldCheck.Platform.Shared.Resources.Errors;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace GoldMetrics.GoldCheck.Platform.ConsumerTraceability.Application.Internal.CommandServices;

public class JewelryProductCommandService(
    IJewelryProductRepository productRepository,
    ITraceabilityJourneyRepository journeyRepository,
    IUnitOfWork unitOfWork,
    IStringLocalizer<ErrorMessages> localizer)
    : IJewelryProductCommandService
{
    // ── ScanProductQR ─────────────────────────────────────────────────────────
    // Creates or updates JewelryProduct AND creates a TraceabilityJourney entry.

    public async Task<Result<JewelryProduct>> Handle(
        ScanProductQRCommand command,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var existing = await productRepository.FindByQRCodeAsync(
                command.QRCode, cancellationToken);

            JewelryProduct product;
            if (existing is null)
            {
                product = new JewelryProduct(command);
                await productRepository.AddAsync(product, cancellationToken);
            }
            else
            {
                existing.RecordScan(command);
                product = existing;
            }

            // Always record a journey entry for every scan
            var journey = new TraceabilityJourney(command);
            await journeyRepository.AddAsync(journey, cancellationToken);

            await unitOfWork.CompleteAsync(cancellationToken);
            return Result<JewelryProduct>.Success(product);
        }
        catch (ArgumentException)
        {
            return Result<JewelryProduct>.Failure(
                ConsumerTraceabilityError.InvalidQRCode,
                localizer[nameof(ConsumerTraceabilityError.InvalidQRCode)]);
        }
        catch (OperationCanceledException)
        {
            return Result<JewelryProduct>.Failure(
                ConsumerTraceabilityError.OperationCancelled,
                localizer[nameof(ConsumerTraceabilityError.OperationCancelled)]);
        }
        catch (DbUpdateException)
        {
            return Result<JewelryProduct>.Failure(
                ConsumerTraceabilityError.DatabaseError,
                localizer[nameof(ConsumerTraceabilityError.DatabaseError)]);
        }
        catch (Exception)
        {
            return Result<JewelryProduct>.Failure(
                ConsumerTraceabilityError.InternalServerError,
                localizer[nameof(ConsumerTraceabilityError.InternalServerError)]);
        }
    }
}