using GoldMetrics.GoldCheck.Platform.MonitoringTelemetry.Domain.Model.Aggregates;
using GoldMetrics.GoldCheck.Platform.MonitoringTelemetry.Domain.Repositories;
using GoldMetrics.GoldCheck.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using GoldMetrics.GoldCheck.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using Microsoft.EntityFrameworkCore;

namespace GoldMetrics.GoldCheck.Platform.MonitoringTelemetry.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

public class TelemetryDataRepository(AppDbContext context)
    : BaseRepository<TelemetryData>(context), ITelemetryDataRepository
{
    public async Task<TelemetryData?> FindByIdAsync(int id, CancellationToken cancellationToken = default)
        => await Context.Set<TelemetryData>().FirstOrDefaultAsync(d => d.Id == id, cancellationToken);

    public async Task<TelemetryData?> FindByTelemetryDataIdAsync(string telemetryDataId, CancellationToken cancellationToken = default)
        => await Context.Set<TelemetryData>().FirstOrDefaultAsync(d => d.TelemetryDataId == telemetryDataId, cancellationToken);

    public async Task<IEnumerable<TelemetryData>> FindByAssetIdAsync(string assetId, CancellationToken cancellationToken = default)
        => await Context.Set<TelemetryData>().Where(d => d.AssetId.Value == assetId).ToListAsync(cancellationToken);

    public async Task<IEnumerable<TelemetryData>> FindAllAsync(CancellationToken cancellationToken = default)
        => await Context.Set<TelemetryData>().ToListAsync(cancellationToken);
}