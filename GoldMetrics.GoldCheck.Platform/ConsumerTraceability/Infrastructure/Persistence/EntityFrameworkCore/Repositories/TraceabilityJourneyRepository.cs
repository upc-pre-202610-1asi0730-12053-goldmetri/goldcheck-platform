using GoldMetrics.GoldCheck.Platform.ConsumerTraceability.Domain.Model.Aggregates;
using GoldMetrics.GoldCheck.Platform.ConsumerTraceability.Domain.Repositories;
using GoldMetrics.GoldCheck.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using GoldMetrics.GoldCheck.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using Microsoft.EntityFrameworkCore;

namespace GoldMetrics.GoldCheck.Platform.ConsumerTraceability.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

public class TraceabilityJourneyRepository(AppDbContext context)
    : BaseRepository<TraceabilityJourney>(context), ITraceabilityJourneyRepository
{
    public async Task<TraceabilityJourney?> FindLatestByQRCodeAsync(
        string qrCode,
        CancellationToken cancellationToken = default)
        => await Context.Set<TraceabilityJourney>()
            .Where(j => j.ProductQRCode.Value == qrCode)
            .OrderByDescending(j => j.CreatedAt)
            .FirstOrDefaultAsync(cancellationToken);
}
