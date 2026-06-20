using GoldMetrics.GoldCheck.Platform.MonitoringTelemetry.Domain.Model.Aggregates;
using GoldMetrics.GoldCheck.Platform.MonitoringTelemetry.Domain.Repositories;
using GoldMetrics.GoldCheck.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using GoldMetrics.GoldCheck.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using Microsoft.EntityFrameworkCore;

namespace GoldMetrics.GoldCheck.Platform.MonitoringTelemetry.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

public class SpeedReadingRepository(AppDbContext context)
    : BaseRepository<SpeedReading>(context), ISpeedReadingRepository
{
    public async Task<SpeedReading?> FindByIdAsync(int id, CancellationToken cancellationToken = default)
        => await Context.Set<SpeedReading>().FirstOrDefaultAsync(r => r.Id == id, cancellationToken);

    public async Task<IEnumerable<SpeedReading>> FindByAssetIdAsync(string assetId, CancellationToken cancellationToken = default)
        => await Context.Set<SpeedReading>().Where(r => r.AssetId.Value == assetId).ToListAsync(cancellationToken);

    public async Task<IEnumerable<SpeedReading>> FindAllAsync(CancellationToken cancellationToken = default)
        => await Context.Set<SpeedReading>().ToListAsync(cancellationToken);
}