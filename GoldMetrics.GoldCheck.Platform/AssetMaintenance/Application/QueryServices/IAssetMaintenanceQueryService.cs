using GoldMetrics.GoldCheck.Platform.AssetMaintenance.Domain.Model.Aggregates;
using GoldMetrics.GoldCheck.Platform.AssetMaintenance.Domain.Model.Queries;

namespace GoldMetrics.GoldCheck.Platform.AssetMaintenance.Application.QueryServices;

public interface IAssetMaintenanceQueryService
{
    Task<Machinery?> Handle(GetMachineryByIdQuery query, CancellationToken cancellationToken);
    Task<IEnumerable<Machinery>> Handle(GetAllMachineryQuery query, CancellationToken cancellationToken);
    Task<IEnumerable<Machinery>> Handle(GetMachineryByStatusQuery query, CancellationToken cancellationToken);
}