using GoldMetrics.GoldCheck.Platform.JewelryInventory.Domain.Model.Aggregates;
using GoldMetrics.GoldCheck.Platform.Shared.Domain.Repositories;

namespace GoldMetrics.GoldCheck.Platform.JewelryInventory.Domain.Repositories;

public interface IJewelryMaterialRepository : IBaseRepository<JewelryMaterial>
{
    Task<JewelryMaterial?> FindByMaterialIdAsync(string materialId,
        CancellationToken cancellationToken = default);

    Task<IEnumerable<JewelryMaterial>> FindAllAsync(
        CancellationToken cancellationToken = default);
}
