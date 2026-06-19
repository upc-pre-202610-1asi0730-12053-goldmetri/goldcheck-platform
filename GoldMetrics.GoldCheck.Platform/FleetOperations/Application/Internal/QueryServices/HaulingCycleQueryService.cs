using GoldMetrics.GoldCheck.Platform.FleetOperations.Application.QueryServices;
using GoldMetrics.GoldCheck.Platform.FleetOperations.Domain.Model.Aggregates;
using GoldMetrics.GoldCheck.Platform.FleetOperations.Domain.Model.Queries;
using GoldMetrics.GoldCheck.Platform.FleetOperations.Domain.Repositories;

namespace GoldMetrics.GoldCheck.Platform.FleetOperations.Application.Internal.QueryServices;

public class HaulingCycleQueryService(IHaulingCycleRepository haulingCycleRepository) : IHaulingCycleQueryService
{
    public async Task<HaulingCycle?> Handle(GetHaulingCycleByIdQuery query, CancellationToken cancellationToken)
    {
        return await haulingCycleRepository.FindByIdAsync(query.Id, cancellationToken);
    }

    public async Task<IEnumerable<HaulingCycle>> Handle(GetAllHaulingCyclesQuery query, CancellationToken cancellationToken)
    {
        return await haulingCycleRepository.ListAsync(cancellationToken);
    }
}
