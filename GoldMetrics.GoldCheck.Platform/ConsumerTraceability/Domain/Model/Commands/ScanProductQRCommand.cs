namespace GoldMetrics.GoldCheck.Platform.ConsumerTraceability.Domain.Model.Commands;

public record ScanProductQRCommand(string QRCode, string ConsumerId);