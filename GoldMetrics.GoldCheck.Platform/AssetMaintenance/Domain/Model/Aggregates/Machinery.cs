using GoldMetrics.GoldCheck.Platform.AssetMaintenance.Domain.Model.Commands;
using GoldMetrics.GoldCheck.Platform.AssetMaintenance.Domain.Model.ValueObjects;

namespace GoldMetrics.GoldCheck.Platform.AssetMaintenance.Domain.Model.Aggregates;

public partial class Machinery
{
    public Machinery()
    {
        MachineryId = new MachineryId();
        EngineHours = new EngineHours();
        MaintenanceStatus = new MaintenanceStatus();
        Model = string.Empty;
        OEM = string.Empty;
        Status = string.Empty;
    }

    public Machinery(RegisterMachineryCommand command)
    {
        MachineryId = new MachineryId(command.MachineryId);
        Model = command.Model;
        OEM = command.OEM;
        EngineHours = new EngineHours();
        MaintenanceStatus = new MaintenanceStatus("Active");
        Status = "MachineryRegistered";
    }

    public int Id { get; }
    public MachineryId MachineryId { get; private set; }
    public string Model { get; private set; }
    public string OEM { get; private set; }
    public EngineHours EngineHours { get; private set; }
    public MaintenanceStatus MaintenanceStatus { get; private set; }
    public string Status { get; private set; }
}