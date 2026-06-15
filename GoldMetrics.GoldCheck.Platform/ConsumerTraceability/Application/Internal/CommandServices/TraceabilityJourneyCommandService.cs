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

public class TraceabilityJourneyCommandService(
    ITraceabilityJourneyRepository journeyRepository,
    IUnitOfWork unitOfWork,
    IStringLocalizer<ErrorMessages> localizer)
    : ITraceabilityJourneyCommandService
{
    // ── RequestJourney ────────────────────────────────────────────────────────

    public async Task<Result<TraceabilityJourney>> Handle(
        RequestJourneyCommand command,
        CancellationToken cancellationToken = default)
    {
        var journey = new TraceabilityJourney(command);
        try
        {
            await journeyRepository.AddAsync(journey, cancellationToken);
            await unitOfWork.CompleteAsync(cancellationToken);
            return Result<TraceabilityJourney>.Success(journey);
        }
        catch (OperationCanceledException)
        {
            return Result<TraceabilityJourney>.Failure(
                ConsumerTraceabilityError.OperationCancelled,
                localizer[nameof(ConsumerTraceabilityError.OperationCancelled)]);
        }
        catch (DbUpdateException)
        {
            return Result<TraceabilityJourney>.Failure(
                ConsumerTraceabilityError.DatabaseError,
                localizer[nameof(ConsumerTraceabilityError.DatabaseError)]);
        }
        catch (Exception)
        {
            return Result<TraceabilityJourney>.Failure(
                ConsumerTraceabilityError.InternalServerError,
                localizer[nameof(ConsumerTraceabilityError.InternalServerError)]);
        }
    }
}