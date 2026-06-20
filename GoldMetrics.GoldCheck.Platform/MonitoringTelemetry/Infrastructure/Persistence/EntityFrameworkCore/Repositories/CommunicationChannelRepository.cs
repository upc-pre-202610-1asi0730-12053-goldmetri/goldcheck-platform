using GoldMetrics.GoldCheck.Platform.MonitoringTelemetry.Domain.Model.Aggregates;
using GoldMetrics.GoldCheck.Platform.MonitoringTelemetry.Domain.Repositories;
using GoldMetrics.GoldCheck.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using GoldMetrics.GoldCheck.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using Microsoft.EntityFrameworkCore;

namespace GoldMetrics.GoldCheck.Platform.MonitoringTelemetry.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

public class CommunicationChannelRepository(AppDbContext context)
    : BaseRepository<CommunicationChannel>(context), ICommunicationChannelRepository
{
    public async Task<CommunicationChannel?> FindByIdAsync(int id, CancellationToken cancellationToken = default)
        => await Context.Set<CommunicationChannel>().FirstOrDefaultAsync(c => c.Id == id, cancellationToken);

    public async Task<IEnumerable<CommunicationChannel>> FindByAssetIdAsync(string assetId, CancellationToken cancellationToken = default)
        => await Context.Set<CommunicationChannel>().Where(c => c.AssetId.Value == assetId).ToListAsync(cancellationToken);

    public async Task<IEnumerable<CommunicationChannel>> FindAllAsync(CancellationToken cancellationToken = default)
        => await Context.Set<CommunicationChannel>().ToListAsync(cancellationToken);
}