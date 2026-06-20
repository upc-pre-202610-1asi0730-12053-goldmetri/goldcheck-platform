namespace GoldMetrics.GoldCheck.Platform.ConsumerTraceability.Domain.Model.Commands;

public record RequestJourneyCommand(string QRCode, string ConsumerId);