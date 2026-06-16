using GoldMetrics.GoldCheck.Platform.JewelryInventory.Domain.Model.Aggregates;
using GoldMetrics.GoldCheck.Platform.Shared.Domain.Repositories;

namespace GoldMetrics.GoldCheck.Platform.JewelryInventory.Domain.Repositories;

public interface IJewelryRepository : IBaseRepository<Jewelry>
{
    Task<Jewelry?> FindByCertificateIdAsync(string certificateId,
        CancellationToken cancellationToken = default);
}
