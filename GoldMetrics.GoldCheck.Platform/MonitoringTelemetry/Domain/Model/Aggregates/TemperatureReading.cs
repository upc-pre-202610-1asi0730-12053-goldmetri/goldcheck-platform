using GoldMetrics.GoldCheck.Platform.MonitoringTelemetry.Domain.Model.Commands;
using GoldMetrics.GoldCheck.Platform.MonitoringTelemetry.Domain.Model.ValueObjects;

namespace GoldMetrics.GoldCheck.Platform.MonitoringTelemetry.Domain.Model.Aggregates;

public partial class TemperatureReading
{
    public TemperatureReading() { AssetId = new AssetId(); Status = string.Empty; }

    public TemperatureReading(MonitorEngineTemperatureCommand command)
    {
        AssetId = new AssetId(command.AssetId);
        Status = "Monitoring";
    }

    public int Id { get; }
    public AssetId AssetId { get; private set; }
    public string Status { get; private set; }

    public void ResetMonitoring() => Status = "Monitoring";
    
    public decimal? ExhaustCelsius { get; private set; }
    
    public void AnalyseExhaust(AnalyseExhaustTemperatureCommand command)
    {
        ExhaustCelsius = new Temperature(command.ExhaustCelsius).Celsius;
        Status = "ExhaustAnalysed";
    }
    
    public decimal? ExhaustLimitCelsius { get; private set; }
    public int? CylinderNumber { get; private set; }
    
    public void AnalyseExhaustLimitPerCylinder(AnalyseExhaustTemperatureLimitPerCylinderCommand command)
    {
        ExhaustLimitCelsius = new Temperature(command.LimitCelsius).Celsius;
        CylinderNumber = command.CylinderNumber;
        Status = "ExhaustLimitAnalysed";
    }
    
    public decimal? RefrigerantCelsius { get; private set; }
    
    public void AnalyseRefrigerant(AnalyseEngineRefrigerantTemperatureCommand command)
    {
        RefrigerantCelsius = new Temperature(command.RefrigerantCelsius).Celsius;
        Status = "RefrigerantAnalysed";
    }
}