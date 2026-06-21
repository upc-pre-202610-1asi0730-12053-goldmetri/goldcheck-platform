namespace GoldMetrics.GoldCheck.Platform.FleetOperations.Interfaces.Acl;

public interface IFleetOperationsContextFacade
{
    Task<bool> ValidateHaulingCycleExists(int haulingCycleId, CancellationToken cancellationToken);
    Task<string> FetchHaulingCycleStatusById(int haulingCycleId, CancellationToken cancellationToken);
}
