using GoldMetrics.GoldCheck.Platform.MonitoringTelemetry.Domain.Model.Commands;
using GoldMetrics.GoldCheck.Platform.MonitoringTelemetry.Domain.Model.ValueObjects;

namespace GoldMetrics.GoldCheck.Platform.MonitoringTelemetry.Domain.Model.Aggregates;

public partial class CommunicationChannel
{
    public CommunicationChannel() { AssetId = new AssetId(); Status = string.Empty; }

    public CommunicationChannel(MonitorCommunicationChannelCommand command)
    {
        AssetId = new AssetId(command.AssetId);
        Status = "Monitoring";
    }

    public int Id { get; }
    public AssetId AssetId { get; private set; }
    public string Status { get; private set; }

    public void ResetMonitoring() => Status = "Monitoring";
    
    public string? LastAnalysedProtocol { get; private set; }
    public void AnalyseCommunication(AnalyseCommunicationCommand command)
    {
        LastAnalysedProtocol = new CommunicationProtocol(command.Protocol).Value;
        Status = $"{LastAnalysedProtocol}Analysed";
    }
    
    public string? AnomalyProtocol { get; private set; }
    public string? AnomalyDescription { get; private set; }

    public void DetectAnomaly(DetectCommunicationAnomalyCommand command)
    {
        AnomalyProtocol = new CommunicationProtocol(command.Protocol).Value;
        AnomalyDescription = command.AnomalyDescription;
        Status = "AnomalyDetected";
    }

    public void LogAnomaly(LogCommunicationAnomalyCommand command)
    {
        AnomalyDescription = command.AnomalyDescription;
        Status = "AnomalyLogged";
    }
}