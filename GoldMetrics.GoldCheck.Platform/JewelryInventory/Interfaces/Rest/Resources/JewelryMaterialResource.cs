namespace GoldMetrics.GoldCheck.Platform.JewelryInventory.Interfaces.Rest.Resources;

public record JewelryMaterialResource(
    int Id,
    string MaterialId,
    string JewelerId,
    string Status,
    string? QRCode,
    string? CertificateId);
