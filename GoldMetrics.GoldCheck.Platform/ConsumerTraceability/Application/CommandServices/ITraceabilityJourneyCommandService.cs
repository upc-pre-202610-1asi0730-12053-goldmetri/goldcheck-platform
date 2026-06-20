using GoldMetrics.GoldCheck.Platform.ConsumerTraceability.Domain.Model.Aggregates;
using GoldMetrics.GoldCheck.Platform.ConsumerTraceability.Domain.Model.Commands;
using GoldMetrics.GoldCheck.Platform.Shared.Application.Model;

namespace GoldMetrics.GoldCheck.Platform.ConsumerTraceability.Application.CommandServices;

public interface ITraceabilityJourneyCommandService
{
    Task<Result<TraceabilityJourney>> Handle(RequestJourneyCommand command,
        CancellationToken cancellationToken = default);
    /// <summary>
    /// Validates and acknowledges a detected language. No persistence — used for flow control only.
    /// </summary>
    Task<Result<string>> Handle(DetectLanguageCommand command,
        CancellationToken cancellationToken = default);
}