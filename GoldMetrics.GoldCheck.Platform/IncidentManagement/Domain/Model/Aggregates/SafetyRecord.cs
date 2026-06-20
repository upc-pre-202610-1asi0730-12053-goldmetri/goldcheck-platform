using GoldMetrics.GoldCheck.Platform.IncidentManagement.Domain.Model.Commands;
using GoldMetrics.GoldCheck.Platform.IncidentManagement.Domain.Model.ValueObjects;

namespace GoldMetrics.GoldCheck.Platform.IncidentManagement.Domain.Model.Aggregates;

public partial class SafetyRecord
{
    public SafetyRecord()
    {
        IncidentType = new IncidentType();
        OperatorId = new OperatorId();
        AssetId = new AssetId();
        RiskLevel = new RiskLevel();
        Status = string.Empty;
    }

    public SafetyRecord(DetectDriverFatigueCommand command)
    {
        IncidentType = new IncidentType("Fatigue");
        OperatorId = new OperatorId(command.OperatorId);
        AssetId = new AssetId(command.AssetId);
        RiskLevel = new RiskLevel("Low");
        Status = "FatigueDetected";
    }

    public int Id { get; }
    public IncidentType IncidentType { get; private set; }
    public OperatorId OperatorId { get; private set; }
    public AssetId AssetId { get; private set; }
    public RiskLevel RiskLevel { get; private set; }
    public string Status { get; private set; }
    
    public void EscalateRiskLevel(EscalateRiskLevelCommand command)
    {
        RiskLevel = new RiskLevel(command.NewRiskLevel);
        Status = "RiskLevelEscalated";
    }
    
    public void EvaluateSafetyRisk(EvaluateSafetyRiskCommand command)
    {
        Status = "RiskLevelUpdated";
    }
    
    public void SendRiskLevelAlert(SendRiskLevelAlertCommand command)
    {
        Status = "RiskLevelAlertCommitted";
    }
    
    public SafetyRecord(DetectSmokeCommand command)
    {
        IncidentType = new IncidentType("Smoke");
        OperatorId = new OperatorId();
        AssetId = new AssetId(command.AssetId);
        RiskLevel = new RiskLevel("High");
        Status = "SmokeDetected";
    }
}