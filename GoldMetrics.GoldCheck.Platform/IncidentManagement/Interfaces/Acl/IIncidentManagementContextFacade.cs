namespace GoldMetrics.GoldCheck.Platform.IncidentManagement.Interfaces.Acl;

public interface IIncidentManagementContextFacade
{
    Task<bool> ValidateIncidentExists(int incidentId, CancellationToken cancellationToken);
    Task<string> FetchIncidentTypeById(int incidentId, CancellationToken cancellationToken);
}
