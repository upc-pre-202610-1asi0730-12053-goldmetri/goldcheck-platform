using GoldMetrics.GoldCheck.Platform.Analytics.Domain.Model.Aggregates;
using GoldMetrics.GoldCheck.Platform.Analytics.Domain.Repositories;
using GoldMetrics.GoldCheck.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using GoldMetrics.GoldCheck.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using Microsoft.EntityFrameworkCore;

namespace GoldMetrics.GoldCheck.Platform.Analytics.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

public class MaterialRepository(AppDbContext context) : BaseRepository<Material>(context), IMaterialRepository
{
    public async Task<Material?> FindByRouteIdAsync(string routeId, CancellationToken cancellationToken = default)
        => await Context.Set<Material>().FirstOrDefaultAsync(m => m.RouteId.Value == routeId, cancellationToken);
    public async Task<IEnumerable<Material>> FindBySupervisorIdAsync(string supervisorId, CancellationToken cancellationToken = default)
        => await Context.Set<Material>().Where(m => m.SupervisorId.Value == supervisorId).ToListAsync(cancellationToken);
}