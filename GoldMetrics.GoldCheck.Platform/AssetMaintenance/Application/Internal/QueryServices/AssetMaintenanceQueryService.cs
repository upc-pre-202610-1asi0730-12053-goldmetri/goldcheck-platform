using GoldMetrics.GoldCheck.Platform.AssetMaintenance.Application.QueryServices;
using GoldMetrics.GoldCheck.Platform.AssetMaintenance.Domain.Model.Aggregates;
using GoldMetrics.GoldCheck.Platform.AssetMaintenance.Domain.Model.Queries;
using GoldMetrics.GoldCheck.Platform.AssetMaintenance.Domain.Repositories;

namespace GoldMetrics.GoldCheck.Platform.AssetMaintenance.Application.Internal.QueryServices;

public class AssetMaintenanceQueryService(IMachineryRepository machineryRepository) : IAssetMaintenanceQueryService
{
    public async Task<Machinery?> Handle(GetMachineryByIdQuery query, CancellationToken cancellationToken)
        => await machineryRepository.FindByMachineryIdAsync(query.MachineryId, cancellationToken);
}