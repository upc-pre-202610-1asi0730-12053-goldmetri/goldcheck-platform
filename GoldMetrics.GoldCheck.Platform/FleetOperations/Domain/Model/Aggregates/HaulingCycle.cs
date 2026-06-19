using GoldMetrics.GoldCheck.Platform.FleetOperations.Domain.Model.Commands;
using GoldMetrics.GoldCheck.Platform.FleetOperations.Domain.Model.ValueObjects;

namespace GoldMetrics.GoldCheck.Platform.FleetOperations.Domain.Model.Aggregates;

public partial class HaulingCycle
{
    public HaulingCycle()
    {
        VehicleId = new VehicleId();
        LoadingPoint = new LoadingPoint();
        RouteProgress = string.Empty;
        Status = string.Empty;
    }

    public HaulingCycle(StartHaulingCycleCommand command)
    {
        VehicleId = new VehicleId(command.VehicleId);
        LoadingPoint = new LoadingPoint(command.LoadingPoint);
        RouteProgress = string.Empty;
        Status = "Started";
    }

    public int Id { get; }
    public VehicleId VehicleId { get; private set; }
    public LoadingPoint LoadingPoint { get; private set; }
    public string RouteProgress { get; private set; }
    public string Status { get; private set; }
    public decimal? PayloadTons { get; private set; }
    public string? DumpingPointName { get; private set; }

    public void LoadMaterial(LoadMaterialCommand command)
    {
        var payload = new Payload(command.PayloadTons);
        PayloadTons = payload.Tons;
        Status = "MaterialLoaded";
    }

    public void Complete(CompleteHaulingCycleCommand command)
    {
        var dumpingPoint = new DumpingPoint(command.DumpingPoint);
        DumpingPointName = dumpingPoint.Name;
        Status = "Completed";
    }
}
