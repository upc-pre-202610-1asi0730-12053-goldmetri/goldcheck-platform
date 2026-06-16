using GoldMetrics.GoldCheck.Platform.JewelryInventory.Application.QueryServices;
using GoldMetrics.GoldCheck.Platform.JewelryInventory.Domain.Model.Aggregates;
using GoldMetrics.GoldCheck.Platform.JewelryInventory.Domain.Model.Queries;
using GoldMetrics.GoldCheck.Platform.JewelryInventory.Domain.Repositories;

namespace GoldMetrics.GoldCheck.Platform.JewelryInventory.Application.Internal.QueryServices;

public class JewelryMaterialQueryService(IJewelryMaterialRepository materialRepository)
    : IJewelryMaterialQueryService
{
    public async Task<JewelryMaterial?> Handle(
        GetMaterialByIdQuery query,
        CancellationToken cancellationToken = default)
        => await materialRepository.FindByMaterialIdAsync(query.MaterialId, cancellationToken);

    public async Task<IEnumerable<JewelryMaterial>> Handle(
        GetAllMaterialsQuery query,
        CancellationToken cancellationToken = default)
        => await materialRepository.FindAllAsync(cancellationToken);
}
