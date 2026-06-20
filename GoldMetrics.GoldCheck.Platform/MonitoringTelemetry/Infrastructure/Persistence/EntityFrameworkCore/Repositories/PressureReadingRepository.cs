using GoldMetrics.GoldCheck.Platform.MonitoringTelemetry.Domain.Model.Aggregates;
using GoldMetrics.GoldCheck.Platform.MonitoringTelemetry.Domain.Repositories;
using GoldMetrics.GoldCheck.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using GoldMetrics.GoldCheck.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using Microsoft.EntityFrameworkCore;

namespace GoldMetrics.GoldCheck.Platform.MonitoringTelemetry.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

public class PressureReadingRepository(AppDbContext context)
    : BaseRepository<PressureReading>(context), IPressureReadingRepository
{
    public async Task<PressureReading?> FindByIdAsync(int id, CancellationToken cancellationToken = default)
        => await Context.Set<PressureReading>().FirstOrDefaultAsync(r => r.Id == id, cancellationToken);

    public async Task<IEnumerable<PressureReading>> FindByAssetIdAsync(string assetId, CancellationToken cancellationToken = default)
        => await Context.Set<PressureReading>().Where(r => r.AssetId.Value == assetId).ToListAsync(cancellationToken);

    public async Task<IEnumerable<PressureReading>> FindAllAsync(CancellationToken cancellationToken = default)
        => await Context.Set<PressureReading>().ToListAsync(cancellationToken);
}