using GoldMetrics.GoldCheck.Platform.ConsumerTraceability.Application.QueryServices;
using GoldMetrics.GoldCheck.Platform.ConsumerTraceability.Domain.Model.Aggregates;
using GoldMetrics.GoldCheck.Platform.ConsumerTraceability.Domain.Model.Queries;
using GoldMetrics.GoldCheck.Platform.ConsumerTraceability.Domain.Repositories;

namespace GoldMetrics.GoldCheck.Platform.ConsumerTraceability.Application.Internal.QueryServices;

public class TraceabilityJourneyQueryService(ITraceabilityJourneyRepository journeyRepository)
    : ITraceabilityJourneyQueryService
{
    public async Task<TraceabilityJourney?> Handle(
        GetTraceabilityJourneyQuery query,
        CancellationToken cancellationToken = default)
        => await journeyRepository.FindLatestByQRCodeAsync(query.QRCode, cancellationToken);
}