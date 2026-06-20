using GoldMetrics.GoldCheck.Platform.IncidentManagement.Domain.Model.Aggregates;
using GoldMetrics.GoldCheck.Platform.IncidentManagement.Domain.Repositories;
using GoldMetrics.GoldCheck.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using GoldMetrics.GoldCheck.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Repositories;
using Microsoft.EntityFrameworkCore;

namespace GoldMetrics.GoldCheck.Platform.IncidentManagement.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

public class SafetyRecordRepository(AppDbContext context) : BaseRepository<SafetyRecord>(context), ISafetyRecordRepository
{
    public async Task<IEnumerable<SafetyRecord>> FindByIncidentTypeAsync(string incidentType, CancellationToken cancellationToken = default)
        => await Context.Set<SafetyRecord>()
            .Where(s => s.IncidentType.Value == incidentType)
            .ToListAsync(cancellationToken);

    public async Task<IEnumerable<SafetyRecord>> FindByRiskLevelAsync(string riskLevel, CancellationToken cancellationToken = default)
        => await Context.Set<SafetyRecord>()
            .Where(s => s.RiskLevel.Value == riskLevel)
            .ToListAsync(cancellationToken);
}