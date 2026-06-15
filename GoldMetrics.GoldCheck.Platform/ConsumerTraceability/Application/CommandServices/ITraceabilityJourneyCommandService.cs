using GoldMetrics.GoldCheck.Platform.ConsumerTraceability.Domain.Model.Aggregates;
using GoldMetrics.GoldCheck.Platform.ConsumerTraceability.Domain.Model.Commands;
using GoldMetrics.GoldCheck.Platform.Shared.Application.Model;

namespace GoldMetrics.GoldCheck.Platform.ConsumerTraceability.Application.CommandServices;

public interface ITraceabilityJourneyCommandService
{
    Task<Result<TraceabilityJourney>> Handle(RequestJourneyCommand command,
        CancellationToken cancellationToken = default);
}