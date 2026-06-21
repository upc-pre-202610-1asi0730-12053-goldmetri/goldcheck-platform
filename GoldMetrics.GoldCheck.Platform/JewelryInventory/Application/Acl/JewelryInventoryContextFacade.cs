using GoldMetrics.GoldCheck.Platform.JewelryInventory.Application.QueryServices;
using GoldMetrics.GoldCheck.Platform.JewelryInventory.Domain.Model.Queries;
using GoldMetrics.GoldCheck.Platform.JewelryInventory.Interfaces.Acl;

namespace GoldMetrics.GoldCheck.Platform.JewelryInventory.Application.Acl;

public class JewelryInventoryContextFacade(IJewelryQueryService jewelryQueryService)
    : IJewelryInventoryContextFacade
{
    public async Task<bool> ValidateCertificateExists(string certificateId, CancellationToken cancellationToken)
    {
        var query = new GetCertificateByIdQuery(certificateId);
        var result = await jewelryQueryService.Handle(query, cancellationToken);
        return result is not null;
    }

    public async Task<string> FetchJewelerIdByCertificateId(string certificateId, CancellationToken cancellationToken)
    {
        var query = new GetCertificateByIdQuery(certificateId);
        var result = await jewelryQueryService.Handle(query, cancellationToken);
        return result?.JewelerId.Value ?? string.Empty;
    }
}
