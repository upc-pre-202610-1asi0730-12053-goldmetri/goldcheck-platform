namespace GoldMetrics.GoldCheck.Platform.AssetMaintenance.Interfaces.Acl;

public interface IAssetMaintenanceContextFacade
{
    Task<bool> ValidateMachineryExists(string machineryId, CancellationToken cancellationToken);
    Task<string> FetchMaintenanceStatusByMachineryId(string machineryId, CancellationToken cancellationToken);
}
