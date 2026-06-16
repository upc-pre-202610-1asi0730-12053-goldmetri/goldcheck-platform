using GoldMetrics.GoldCheck.Platform.JewelryInventory.Domain.Model.Aggregates;
using GoldMetrics.GoldCheck.Platform.JewelryInventory.Domain.Model.Queries;

namespace GoldMetrics.GoldCheck.Platform.JewelryInventory.Application.QueryServices;

public interface IJewelryQueryService
{
    Task<Jewelry?> Handle(GetCertificateByIdQuery query,
        CancellationToken cancellationToken = default);
}
