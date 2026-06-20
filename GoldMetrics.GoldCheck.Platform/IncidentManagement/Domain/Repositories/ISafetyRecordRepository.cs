using GoldMetrics.GoldCheck.Platform.IncidentManagement.Domain.Model.Aggregates;
using GoldMetrics.GoldCheck.Platform.Shared.Domain.Repositories;

namespace GoldMetrics.GoldCheck.Platform.IncidentManagement.Domain.Repositories;

public interface ISafetyRecordRepository : IBaseRepository<SafetyRecord>
{
    Task<IEnumerable<SafetyRecord>> FindByIncidentTypeAsync(string incidentType, CancellationToken cancellationToken = default);
    Task<IEnumerable<SafetyRecord>> FindByRiskLevelAsync(string riskLevel, CancellationToken cancellationToken = default);
}