using GoldMetrics.GoldCheck.Platform.MaterialOperations.Domain.Model.Aggregates;
using GoldMetrics.GoldCheck.Platform.Shared.Domain.Repositories;

namespace GoldMetrics.GoldCheck.Platform.MaterialOperations.Domain.Repositories;

public interface IMaterialRepository : IBaseRepository<Material>
{
    Task<Material?> FindByBatchIdAsync(string batchId, CancellationToken cancellationToken = default);
}
