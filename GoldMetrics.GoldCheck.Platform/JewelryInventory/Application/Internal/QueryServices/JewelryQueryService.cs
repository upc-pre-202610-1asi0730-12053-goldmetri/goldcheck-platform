using GoldMetrics.GoldCheck.Platform.JewelryInventory.Application.QueryServices;
using GoldMetrics.GoldCheck.Platform.JewelryInventory.Domain.Model.Aggregates;
using GoldMetrics.GoldCheck.Platform.JewelryInventory.Domain.Model.Queries;
using GoldMetrics.GoldCheck.Platform.JewelryInventory.Domain.Repositories;

namespace GoldMetrics.GoldCheck.Platform.JewelryInventory.Application.Internal.QueryServices;

public class JewelryQueryService(IJewelryRepository jewelryRepository)
    : IJewelryQueryService
{
    public async Task<Jewelry?> Handle(
        GetCertificateByIdQuery query,
        CancellationToken cancellationToken = default)
        => await jewelryRepository.FindByCertificateIdAsync(query.CertificateId, cancellationToken);
}
