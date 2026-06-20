using GoldMetrics.GoldCheck.Platform.MonitoringTelemetry.Domain.Model.Commands;
using GoldMetrics.GoldCheck.Platform.MonitoringTelemetry.Domain.Model.ValueObjects;

namespace GoldMetrics.GoldCheck.Platform.MonitoringTelemetry.Domain.Model.Aggregates;

public partial class PressureReading
{
    public PressureReading() { AssetId = new AssetId(); Status = string.Empty; }

    public PressureReading(MonitorPressureCommand command)
    {
        AssetId = new AssetId(command.AssetId);
        Status = "Monitoring";
    }

    public int Id { get; }
    public AssetId AssetId { get; private set; }
    public string Status { get; private set; }

    public void ResetMonitoring() => Status = "Monitoring";
}