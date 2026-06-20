using GoldMetrics.GoldCheck.Platform.MonitoringTelemetry.Domain.Model.Commands;
using GoldMetrics.GoldCheck.Platform.MonitoringTelemetry.Domain.Model.ValueObjects;

namespace GoldMetrics.GoldCheck.Platform.MonitoringTelemetry.Domain.Model.Aggregates;

public partial class TelemetryData
{
    public TelemetryData() { AssetId = new AssetId(); RawData = string.Empty; TelemetryDataId = string.Empty; Status = string.Empty; }

    public TelemetryData(ProcessTelemetryDataCommand command)
    {
        AssetId = new AssetId(command.AssetId);
        TelemetryDataId = Guid.NewGuid().ToString();
        RawData = command.RawData;
        Status = "Processed";
        IsValidated = false;
    }

    public int Id { get; }
    public AssetId AssetId { get; private set; }
    public string TelemetryDataId { get; private set; }
    public string RawData { get; private set; }
    public string Status { get; private set; }
    public bool IsValidated { get; private set; }
    
    public void Validate(ValidateTelemetryDataCommand command)
    {
        IsValidated = true;
        Status = "Validated";
    }
}