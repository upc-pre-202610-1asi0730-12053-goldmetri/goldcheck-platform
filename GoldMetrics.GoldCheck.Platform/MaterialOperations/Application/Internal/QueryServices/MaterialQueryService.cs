using GoldMetrics.GoldCheck.Platform.MaterialOperations.Application.QueryServices;
using GoldMetrics.GoldCheck.Platform.MaterialOperations.Domain.Model.Aggregates;
using GoldMetrics.GoldCheck.Platform.MaterialOperations.Domain.Model.Queries;
using GoldMetrics.GoldCheck.Platform.MaterialOperations.Domain.Repositories;

namespace GoldMetrics.GoldCheck.Platform.MaterialOperations.Application.Internal.QueryServices;

public class MaterialQueryService(IMaterialRepository materialRepository) : IMaterialQueryService
{
    public async Task<Material?> Handle(GetMaterialByIdQuery query, CancellationToken cancellationToken)
        => await materialRepository.FindByBatchIdAsync(query.BatchId, cancellationToken);

    public async Task<IEnumerable<Material>> Handle(GetAllMaterialsQuery query, CancellationToken cancellationToken)
        => await materialRepository.ListAsync(cancellationToken);

    public async Task<IEnumerable<Material>> Handle(GetMaterialsByTypeQuery query, CancellationToken cancellationToken)
        => await materialRepository.FindByMineralTypeAsync(query.MineralType, cancellationToken);
}
