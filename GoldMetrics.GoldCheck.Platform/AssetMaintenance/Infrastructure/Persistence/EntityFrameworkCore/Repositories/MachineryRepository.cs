using GoldMetrics.GoldCheck.Platform.AssetMaintenance.Domain.Model.Aggregates;
using GoldMetrics.GoldCheck.Platform.AssetMaintenance.Domain.Repositories;
using GoldMetrics.GoldCheck.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using GoldMetrics.GoldCheck.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using Microsoft.EntityFrameworkCore;

namespace GoldMetrics.GoldCheck.Platform.AssetMaintenance.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

public class MachineryRepository(AppDbContext context) : BaseRepository<Machinery>(context), IMachineryRepository
{
    public async Task<Machinery?> FindByMachineryIdAsync(string machineryId, CancellationToken cancellationToken = default)
        => await Context.Set<Machinery>()
            .FirstOrDefaultAsync(m => m.MachineryId.Value == machineryId, cancellationToken);
    
    public async Task<IEnumerable<Machinery>> FindByStatusAsync(string status, CancellationToken cancellationToken = default)
        => await Context.Set<Machinery>()
            .Where(m => m.MaintenanceStatus.Value == status)
            .ToListAsync(cancellationToken);
}