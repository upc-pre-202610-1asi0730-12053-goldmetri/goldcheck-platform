using GoldMetrics.GoldCheck.Platform.Analytics.Domain.Model.Aggregates;
using GoldMetrics.GoldCheck.Platform.Shared.Domain.Repositories;

namespace GoldMetrics.GoldCheck.Platform.Analytics.Domain.Repositories;

public interface IMaterialRepository : IBaseRepository<Material>
{
    Task<Material?> FindByRouteIdAsync(string routeId, CancellationToken cancellationToken = default);
    Task<IEnumerable<Material>> FindBySupervisorIdAsync(string supervisorId, CancellationToken cancellationToken = default);
    Task<IEnumerable<Material>> FindByProductionPeriodAsync(DateTimeOffset start, DateTimeOffset end, CancellationToken cancellationToken = default);
    Task<Material?> FindByMaterialIdAsync(string materialId, CancellationToken cancellationToken = default);
}