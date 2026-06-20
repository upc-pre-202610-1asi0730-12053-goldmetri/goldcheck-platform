namespace GoldMetrics.GoldCheck.Platform.ConsumerTraceability.Domain.Model.Commands;

public record DetectLanguageCommand(string ConsumerId, string LanguageCode);