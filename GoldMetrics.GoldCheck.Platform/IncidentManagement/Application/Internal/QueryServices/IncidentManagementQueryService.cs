using GoldMetrics.GoldCheck.Platform.IncidentManagement.Application.QueryServices;
using GoldMetrics.GoldCheck.Platform.IncidentManagement.Domain.Model.Aggregates;
using GoldMetrics.GoldCheck.Platform.IncidentManagement.Domain.Model.Queries;
using GoldMetrics.GoldCheck.Platform.IncidentManagement.Domain.Repositories;

namespace GoldMetrics.GoldCheck.Platform.IncidentManagement.Application.Internal.QueryServices;

public class IncidentManagementQueryService(ISafetyRecordRepository safetyRecordRepository)
    : IIncidentManagementQueryService
{
    public async Task<SafetyRecord?> Handle(GetIncidentByIdQuery query, CancellationToken cancellationToken)
        => await safetyRecordRepository.FindByIdAsync(query.Id, cancellationToken);
}