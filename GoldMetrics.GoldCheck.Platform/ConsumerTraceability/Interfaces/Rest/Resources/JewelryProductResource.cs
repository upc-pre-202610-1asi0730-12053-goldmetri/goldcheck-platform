namespace GoldMetrics.GoldCheck.Platform.ConsumerTraceability.Interfaces.Rest.Resources;

public record JewelryProductResource(
    int Id,
    string QRCode,
    string ConsumerId,
    string Status,
    int ScanCount);