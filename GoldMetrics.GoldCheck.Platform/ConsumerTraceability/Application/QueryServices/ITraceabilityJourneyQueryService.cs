using GoldMetrics.GoldCheck.Platform.ConsumerTraceability.Domain.Model.Aggregates;
using GoldMetrics.GoldCheck.Platform.ConsumerTraceability.Domain.Model.Queries;

namespace GoldMetrics.GoldCheck.Platform.ConsumerTraceability.Application.QueryServices;

public interface ITraceabilityJourneyQueryService
{
    Task<TraceabilityJourney?> Handle(GetTraceabilityJourneyQuery query,
        CancellationToken cancellationToken = default);
}