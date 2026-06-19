using GoldMetrics.GoldCheck.Platform.AssetMaintenance.Domain.Model.Aggregates;
using GoldMetrics.GoldCheck.Platform.Shared.Domain.Repositories;

namespace GoldMetrics.GoldCheck.Platform.AssetMaintenance.Domain.Repositories;

public interface IMachineryRepository : IBaseRepository<Machinery>
{
    Task<Machinery?> FindByMachineryIdAsync(string machineryId, CancellationToken cancellationToken = default);
    Task<IEnumerable<Machinery>> FindByStatusAsync(string status, CancellationToken cancellationToken = default);
}