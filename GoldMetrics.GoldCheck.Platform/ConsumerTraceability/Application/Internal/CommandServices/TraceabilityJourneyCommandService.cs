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
using GoldMetrics.GoldCheck.Platform.ConsumerTraceability.Domain.Model.ValueObjects;

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
    
    private static readonly HashSet<string> SupportedLanguages =
        new(StringComparer.OrdinalIgnoreCase) { "en", "es" };

// ── DetectLanguage ────────────────────────────────────────────────────────
// No persistence — validates the language code and acknowledges detection.
// No REST endpoint (internal flow only).

    public Task<Result<string>> Handle(
        DetectLanguageCommand command,
        CancellationToken cancellationToken = default)
    {
        try
        {
            // Validate ISO 639-1 format
            var language = new Language(command.LanguageCode);

            if (!SupportedLanguages.Contains(language.Code))
                return Task.FromResult(Result<string>.Failure(
                    ConsumerTraceabilityError.LanguageNotSupported,
                    localizer[nameof(ConsumerTraceabilityError.LanguageNotSupported)]));

            return Task.FromResult(Result<string>.Success(language.Code));
        }
        catch (ArgumentException)
        {
            return Task.FromResult(Result<string>.Failure(
                ConsumerTraceabilityError.LanguageNotSupported,
                localizer[nameof(ConsumerTraceabilityError.LanguageNotSupported)]));
        }
    }
}