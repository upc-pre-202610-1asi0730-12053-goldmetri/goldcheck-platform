using GoldMetrics.GoldCheck.Platform.AssetMaintenance.Application.QueryServices;
using GoldMetrics.GoldCheck.Platform.AssetMaintenance.Domain.Model.Queries;
using GoldMetrics.GoldCheck.Platform.AssetMaintenance.Interfaces.Acl;

namespace GoldMetrics.GoldCheck.Platform.AssetMaintenance.Application.Acl;

public class AssetMaintenanceContextFacade(IAssetMaintenanceQueryService assetMaintenanceQueryService)
    : IAssetMaintenanceContextFacade
{
    public async Task<bool> ValidateMachineryExists(string machineryId, CancellationToken cancellationToken)
    {
        var query = new GetMachineryByIdQuery(machineryId);
        var result = await assetMaintenanceQueryService.Handle(query, cancellationToken);
        return result is not null;
    }

    public async Task<string> FetchMaintenanceStatusByMachineryId(string machineryId, CancellationToken cancellationToken)
    {
        var query = new GetMachineryByIdQuery(machineryId);
        var result = await assetMaintenanceQueryService.Handle(query, cancellationToken);
        return result?.MaintenanceStatus.Value ?? string.Empty;
    }
}
