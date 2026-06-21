namespace GoldMetrics.GoldCheck.Platform.JewelryInventory.Interfaces.Acl;

public interface IJewelryInventoryContextFacade
{
    Task<bool> ValidateCertificateExists(string certificateId, CancellationToken cancellationToken);
    Task<string> FetchJewelerIdByCertificateId(string certificateId, CancellationToken cancellationToken);
}
