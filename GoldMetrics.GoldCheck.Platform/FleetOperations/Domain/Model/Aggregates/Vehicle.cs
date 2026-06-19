using GoldMetrics.GoldCheck.Platform.FleetOperations.Domain.Model.Commands;
using GoldMetrics.GoldCheck.Platform.FleetOperations.Domain.Model.ValueObjects;

namespace GoldMetrics.GoldCheck.Platform.FleetOperations.Domain.Model.Aggregates;

public partial class Vehicle
{
    public Vehicle()
    {
        VehicleId = new VehicleId();
        OperatorId = new OperatorId();
        Status = string.Empty;
    }

    public Vehicle(AssignVehicleCommand command)
    {
        VehicleId = new VehicleId(command.VehicleId);
        OperatorId = new OperatorId(command.OperatorId);
        IsEngineOn = false;
        Status = "Assigned";
    }

    public int Id { get; }
    public VehicleId VehicleId { get; private set; }
    public OperatorId OperatorId { get; private set; }
    public bool IsEngineOn { get; private set; }
    public string Status { get; private set; }
}
