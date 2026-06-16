namespace GoldMetrics.GoldCheck.Platform.ConsumerTraceability.Interfaces.Rest.Resources;

public record JewelryProductResource(
    int Id,
    string QRCode,
    string ConsumerId,
    string? CertificateId,
    string Status,
    int ScanCount);