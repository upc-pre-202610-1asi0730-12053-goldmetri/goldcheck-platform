using GoldMetrics.GoldCheck.Platform.ConsumerTraceability.Domain.Model.Aggregates;
using GoldMetrics.GoldCheck.Platform.ConsumerTraceability.Domain.Repositories;
using GoldMetrics.GoldCheck.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using GoldMetrics.GoldCheck.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

namespace GoldMetrics.GoldCheck.Platform.ConsumerTraceability.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

public class TraceabilityJourneyRepository(AppDbContext context)
    : BaseRepository<TraceabilityJourney>(context), ITraceabilityJourneyRepository
{
}