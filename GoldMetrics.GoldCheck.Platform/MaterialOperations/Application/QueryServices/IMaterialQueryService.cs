using GoldMetrics.GoldCheck.Platform.MaterialOperations.Domain.Model.Aggregates;
using GoldMetrics.GoldCheck.Platform.MaterialOperations.Domain.Model.Queries;

namespace GoldMetrics.GoldCheck.Platform.MaterialOperations.Application.QueryServices;

public interface IMaterialQueryService
{
    Task<Material?> Handle(GetMaterialByIdQuery query, CancellationToken cancellationToken);
    Task<IEnumerable<Material>> Handle(GetAllMaterialsQuery query, CancellationToken cancellationToken);
}
