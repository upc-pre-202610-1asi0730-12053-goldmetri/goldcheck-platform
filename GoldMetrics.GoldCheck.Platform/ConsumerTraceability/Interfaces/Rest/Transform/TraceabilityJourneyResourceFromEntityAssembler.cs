using GoldMetrics.GoldCheck.Platform.ConsumerTraceability.Domain.Model.Aggregates;
using GoldMetrics.GoldCheck.Platform.ConsumerTraceability.Interfaces.Rest.Resources;

namespace GoldMetrics.GoldCheck.Platform.ConsumerTraceability.Interfaces.Rest.Transform;

public static class TraceabilityJourneyResourceFromEntityAssembler
{
    public static TraceabilityJourneyResource ToResourceFromEntity(TraceabilityJourney entity)
        => new(
            entity.Id,
            entity.ProductQRCode.Value,
            entity.ConsumerId.Value,
            entity.JourneySummary,
            entity.Status);
}