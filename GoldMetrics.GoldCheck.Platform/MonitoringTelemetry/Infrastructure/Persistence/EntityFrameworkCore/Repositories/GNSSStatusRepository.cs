using GoldMetrics.GoldCheck.Platform.MonitoringTelemetry.Domain.Model.Aggregates;
using GoldMetrics.GoldCheck.Platform.MonitoringTelemetry.Domain.Repositories;
using GoldMetrics.GoldCheck.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using GoldMetrics.GoldCheck.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using Microsoft.EntityFrameworkCore;

namespace GoldMetrics.GoldCheck.Platform.MonitoringTelemetry.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

public class GNSSStatusRepository(AppDbContext context)
    : BaseRepository<GNSSStatus>(context), IGNSSStatusRepository
{
    public async Task<GNSSStatus?> FindByIdAsync(int id, CancellationToken cancellationToken = default)
        => await Context.Set<GNSSStatus>().FirstOrDefaultAsync(s => s.Id == id, cancellationToken);

    public async Task<IEnumerable<GNSSStatus>> FindByAssetIdAsync(string assetId, CancellationToken cancellationToken = default)
        => await Context.Set<GNSSStatus>().Where(s => s.AssetId.Value == assetId).ToListAsync(cancellationToken);

    public async Task<IEnumerable<GNSSStatus>> FindAllAsync(CancellationToken cancellationToken = default)
        => await Context.Set<GNSSStatus>().ToListAsync(cancellationToken);
}