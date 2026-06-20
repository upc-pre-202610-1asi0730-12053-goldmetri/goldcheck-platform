using GoldMetrics.GoldCheck.Platform.MonitoringTelemetry.Domain.Model.Commands;
using GoldMetrics.GoldCheck.Platform.MonitoringTelemetry.Domain.Model.ValueObjects;

namespace GoldMetrics.GoldCheck.Platform.MonitoringTelemetry.Domain.Model.Aggregates;

public partial class GNSSStatus
{
    public GNSSStatus() { AssetId = new AssetId(); Status = string.Empty; }

    public GNSSStatus(MonitorGNSSStatusCommand command)
    {
        AssetId = new AssetId(command.AssetId);
        Status = "Active";
        RestartCount = 0;
    }

    public int Id { get; }
    public AssetId AssetId { get; private set; }
    public string Status { get; private set; }
    public int RestartCount { get; private set; }
    public void ResetMonitoring() => Status = "Active";
    
    public void DetectAnomaly(DetectGNSSAnomalyCommand command) =>
        Status = "ChipOff";
    
    public string? RestartReason { get; private set; }

    public void Restart(RestartGNSSCommand command)
    {
        RestartReason = command.RestartReason;
        RestartCount++;
        Status = "Restarted";
    }

    public void LogRestart(LogGNSSRestartCommand command) =>
        Status = "RestartLogged";
}