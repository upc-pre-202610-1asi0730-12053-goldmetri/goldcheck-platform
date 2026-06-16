namespace GoldMetrics.GoldCheck.Platform.JewelryInventory.Interfaces.Rest.Resources;

public record CertificateResource(
    int Id,
    string CertificateId,
    string MaterialId,
    string JewelerId,
    bool IsSigned);
