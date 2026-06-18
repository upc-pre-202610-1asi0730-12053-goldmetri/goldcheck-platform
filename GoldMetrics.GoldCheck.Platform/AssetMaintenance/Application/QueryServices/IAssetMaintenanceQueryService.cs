using GoldMetrics.GoldCheck.Platform.AssetMaintenance.Domain.Model.Aggregates;
using GoldMetrics.GoldCheck.Platform.AssetMaintenance.Domain.Model.Queries;

namespace GoldMetrics.GoldCheck.Platform.AssetMaintenance.Application.QueryServices;

public interface IAssetMaintenanceQueryService
{
    Task<Machinery?> Handle(GetMachineryByIdQuery query, CancellationToken cancellationToken);
}