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
    public decimal? CurrentSpeedKmPerHour { get; private set; }
    public decimal? SpeedLimitKmPerHour { get; private set; }
    public bool IsViolation { get; private set; }
    public string? ViolationDescription { get; private set; }

    public void ResetMonitoring() => Status = "Monitoring";
    public void DetectExcess(DetectSpeedExcessCommand command)
    {
        var speed = new Speed(command.SpeedKmPerHour);
        var limit = new Speed(command.SpeedLimitKmPerHour);
        CurrentSpeedKmPerHour = speed.KmPerHour;
        SpeedLimitKmPerHour = limit.KmPerHour;
        IsViolation = speed.KmPerHour > limit.KmPerHour;
        Status = IsViolation ? "SpeedExcessDetected" : "SpeedNormal";
    }

    public void LogExcess(LogSpeedExcessCommand command)
    {
        var speed = new Speed(command.SpeedKmPerHour);
        ViolationDescription = $"Speed violation: {speed.KmPerHour} km/h exceeded the limit.";
        Status = "ViolationLogged";
    }
}