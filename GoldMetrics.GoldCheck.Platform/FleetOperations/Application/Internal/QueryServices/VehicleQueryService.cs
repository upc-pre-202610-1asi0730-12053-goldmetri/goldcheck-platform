using GoldMetrics.GoldCheck.Platform.FleetOperations.Application.QueryServices;
using GoldMetrics.GoldCheck.Platform.FleetOperations.Domain.Model.Aggregates;
using GoldMetrics.GoldCheck.Platform.FleetOperations.Domain.Model.Queries;
using GoldMetrics.GoldCheck.Platform.FleetOperations.Domain.Repositories;

namespace GoldMetrics.GoldCheck.Platform.FleetOperations.Application.Internal.QueryServices;

public class VehicleQueryService(IVehicleRepository vehicleRepository) : IVehicleQueryService
{
    public async Task<Vehicle?> Handle(GetVehicleByIdQuery query, CancellationToken cancellationToken)
    {
        return await vehicleRepository.FindByVehicleIdAsync(query.VehicleId, cancellationToken);
    }
}
