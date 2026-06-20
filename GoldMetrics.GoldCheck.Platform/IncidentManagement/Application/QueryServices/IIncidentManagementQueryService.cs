using GoldMetrics.GoldCheck.Platform.IncidentManagement.Domain.Model.Aggregates;
using GoldMetrics.GoldCheck.Platform.IncidentManagement.Domain.Model.Queries;

namespace GoldMetrics.GoldCheck.Platform.IncidentManagement.Application.QueryServices;

public interface IIncidentManagementQueryService
{
    Task<SafetyRecord?> Handle(GetIncidentByIdQuery query, CancellationToken cancellationToken);
    Task<IEnumerable<SafetyRecord>> Handle(GetAllIncidentsQuery query, CancellationToken cancellationToken);
}