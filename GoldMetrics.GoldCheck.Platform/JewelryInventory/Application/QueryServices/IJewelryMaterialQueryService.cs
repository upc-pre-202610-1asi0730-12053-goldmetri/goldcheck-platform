using GoldMetrics.GoldCheck.Platform.JewelryInventory.Domain.Model.Aggregates;
using GoldMetrics.GoldCheck.Platform.JewelryInventory.Domain.Model.Queries;

namespace GoldMetrics.GoldCheck.Platform.JewelryInventory.Application.QueryServices;

public interface IJewelryMaterialQueryService
{
    Task<JewelryMaterial?> Handle(GetMaterialByIdQuery query,
        CancellationToken cancellationToken = default);

    Task<IEnumerable<JewelryMaterial>> Handle(GetAllMaterialsQuery query,
        CancellationToken cancellationToken = default);
}
