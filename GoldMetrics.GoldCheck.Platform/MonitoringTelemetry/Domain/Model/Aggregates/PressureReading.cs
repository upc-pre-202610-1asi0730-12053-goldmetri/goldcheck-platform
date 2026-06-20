using GoldMetrics.GoldCheck.Platform.MonitoringTelemetry.Domain.Model.Commands;
using GoldMetrics.GoldCheck.Platform.MonitoringTelemetry.Domain.Model.ValueObjects;

namespace GoldMetrics.GoldCheck.Platform.MonitoringTelemetry.Domain.Model.Aggregates;

public partial class PressureReading
{
    public PressureReading()
    {
        AssetId = new AssetId();
        Status = string.Empty;
    }

    public PressureReading(MonitorPressureCommand command)
    {
        AssetId = new AssetId(command.AssetId);
        Status = "Monitoring";
    }

    public int Id { get; }
    public AssetId AssetId { get; private set; }
    public string Status { get; private set; }
    public decimal? OilFilterDifferenceBar { get; private set; }
    public decimal? OilPanBar { get; private set; }
    public decimal? AbsoluteEngineOilBar { get; private set; }
    public decimal? OilFilterBar { get; private set; }
    public string? AnomalyPressureType { get; private set; }
    public string? AnomalyDescription { get; private set; }

    public void ResetMonitoring() => Status = "Monitoring";
    public void AnalysePressure(AnalysePressureCommand command)
    {
        var pressure = new Pressure(command.PressureBar);
        var pressureType = new PressureType(command.PressureType);
        switch (pressureType.Value)
        {
            case "OilFilterDifference":
                OilFilterDifferenceBar = pressure.Bar;
                break;
            case "OilPan":
                OilPanBar = pressure.Bar;
                break;
            case "AbsoluteEngineOil":
                AbsoluteEngineOilBar = pressure.Bar;
                break;
            case "OilFilter":
                OilFilterBar = pressure.Bar;
                break;
        }
        Status = $"{pressureType.Value}Analysed";
    }
    public void DetectAnomaly(DetectPressureAnomalyCommand command)
    {
        AnomalyPressureType = new PressureType(command.PressureType).Value;
        Status = "AnomalyDetected";
    }

    public void LogAnomaly(LogPressureAnomalyCommand command)
    {
        AnomalyDescription = command.AnomalyDescription;
        Status = "AnomalyLogged";
    }

}