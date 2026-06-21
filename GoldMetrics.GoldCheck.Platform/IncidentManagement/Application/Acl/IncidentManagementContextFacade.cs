using GoldMetrics.GoldCheck.Platform.IncidentManagement.Application.QueryServices;
using GoldMetrics.GoldCheck.Platform.IncidentManagement.Domain.Model.Queries;
using GoldMetrics.GoldCheck.Platform.IncidentManagement.Interfaces.Acl;

namespace GoldMetrics.GoldCheck.Platform.IncidentManagement.Application.Acl;

public class IncidentManagementContextFacade(IIncidentManagementQueryService incidentManagementQueryService)
    : IIncidentManagementContextFacade
{
    public async Task<bool> ValidateIncidentExists(int incidentId, CancellationToken cancellationToken)
    {
        var query = new GetIncidentByIdQuery(incidentId);
        var result = await incidentManagementQueryService.Handle(query, cancellationToken);
        return result is not null;
    }

    public async Task<string> FetchIncidentTypeById(int incidentId, CancellationToken cancellationToken)
    {
        var query = new GetIncidentByIdQuery(incidentId);
        var result = await incidentManagementQueryService.Handle(query, cancellationToken);
        return result?.IncidentType.Value ?? string.Empty;
    }
}
