using GoldMetrics.GoldCheck.Platform.MonitoringTelemetry.Domain.Model.Commands;
using GoldMetrics.GoldCheck.Platform.MonitoringTelemetry.Domain.Model.ValueObjects;

namespace GoldMetrics.GoldCheck.Platform.MonitoringTelemetry.Domain.Model.Aggregates;

public partial class SpeedReading
{
    public SpeedReading() { AssetId = new AssetId(); Status = string.Empty; }

    public SpeedReading(MonitorSpeedStatusCommand command)
    {
        AssetId = new AssetId(command.AssetId);
        Status = "Monitoring";
    }

    public int Id { get; }
    public AssetId AssetId { get; private set; }
    public string Status { get; private set; }

    public void ResetMonitoring() => Status = "Monitoring";
}