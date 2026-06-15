using GoldMetrics.GoldCheck.Platform.MaterialOperations.Domain.Model.Aggregates;
using GoldMetrics.GoldCheck.Platform.MaterialOperations.Domain.Repositories;
using GoldMetrics.GoldCheck.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using GoldMetrics.GoldCheck.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using Microsoft.EntityFrameworkCore;

namespace GoldMetrics.GoldCheck.Platform.MaterialOperations.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

public class MaterialRepository(AppDbContext context) : BaseRepository<Material>(context), IMaterialRepository
{
    public async Task<Material?> FindByBatchIdAsync(string batchId, CancellationToken cancellationToken = default)
        => await Context.Set<Material>()
            .FirstOrDefaultAsync(m => m.BatchId.Value == batchId, cancellationToken);

    public async Task<IEnumerable<Material>> FindByMineralTypeAsync(string mineralType, CancellationToken cancellationToken = default)
        => await Context.Set<Material>()
            .Where(m => m.MineralType.Value == mineralType)
            .ToListAsync(cancellationToken);
}
