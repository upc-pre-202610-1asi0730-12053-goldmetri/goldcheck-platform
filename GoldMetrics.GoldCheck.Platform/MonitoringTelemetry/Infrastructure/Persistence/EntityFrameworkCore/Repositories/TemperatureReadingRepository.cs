using GoldMetrics.GoldCheck.Platform.MonitoringTelemetry.Domain.Model.Aggregates;
using GoldMetrics.GoldCheck.Platform.MonitoringTelemetry.Domain.Repositories;
using GoldMetrics.GoldCheck.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using GoldMetrics.GoldCheck.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using Microsoft.EntityFrameworkCore;

namespace GoldMetrics.GoldCheck.Platform.MonitoringTelemetry.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

public class TemperatureReadingRepository(AppDbContext context)
    : BaseRepository<TemperatureReading>(context), ITemperatureReadingRepository
{
    public async Task<TemperatureReading?> FindByIdAsync(int id, CancellationToken cancellationToken = default)
        => await Context.Set<TemperatureReading>().FirstOrDefaultAsync(r => r.Id == id, cancellationToken);

    public async Task<IEnumerable<TemperatureReading>> FindByAssetIdAsync(string assetId, CancellationToken cancellationToken = default)
        => await Context.Set<TemperatureReading>().Where(r => r.AssetId.Value == assetId).ToListAsync(cancellationToken);

    public async Task<IEnumerable<TemperatureReading>> FindAllAsync(CancellationToken cancellationToken = default)
        => await Context.Set<TemperatureReading>().ToListAsync(cancellationToken);
}