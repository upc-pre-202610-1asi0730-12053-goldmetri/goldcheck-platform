using GoldMetrics.GoldCheck.Platform.FleetOperations.Domain.Model.Aggregates;
using GoldMetrics.GoldCheck.Platform.FleetOperations.Domain.Model.Queries;

namespace GoldMetrics.GoldCheck.Platform.FleetOperations.Application.QueryServices;

public interface IHaulingCycleQueryService
{
    Task<HaulingCycle?> Handle(GetHaulingCycleByIdQuery query, CancellationToken cancellationToken);
    Task<IEnumerable<HaulingCycle>> Handle(GetAllHaulingCyclesQuery query, CancellationToken cancellationToken);
}
