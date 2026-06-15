namespace GoldMetrics.GoldCheck.Platform.ConsumerTraceability.Interfaces.Rest.Resources;

public record TraceabilityJourneyResource(
    int Id,
    string ProductQRCode,
    string ConsumerId,
    string JourneySummary,
    string Status);