using GoldMetrics.GoldCheck.Platform.FleetOperations.Application.QueryServices;
using GoldMetrics.GoldCheck.Platform.FleetOperations.Domain.Model.Queries;
using GoldMetrics.GoldCheck.Platform.FleetOperations.Interfaces.Acl;

namespace GoldMetrics.GoldCheck.Platform.FleetOperations.Application.Acl;

public class FleetOperationsContextFacade(IHaulingCycleQueryService haulingCycleQueryService)
    : IFleetOperationsContextFacade
{
    public async Task<bool> ValidateHaulingCycleExists(int haulingCycleId, CancellationToken cancellationToken)
    {
        var query = new GetHaulingCycleByIdQuery(haulingCycleId);
        var result = await haulingCycleQueryService.Handle(query, cancellationToken);
        return result is not null;
    }

    public async Task<string> FetchHaulingCycleStatusById(int haulingCycleId, CancellationToken cancellationToken)
    {
        var query = new GetHaulingCycleByIdQuery(haulingCycleId);
        var result = await haulingCycleQueryService.Handle(query, cancellationToken);
        return result?.Status ?? string.Empty;
    }
}
