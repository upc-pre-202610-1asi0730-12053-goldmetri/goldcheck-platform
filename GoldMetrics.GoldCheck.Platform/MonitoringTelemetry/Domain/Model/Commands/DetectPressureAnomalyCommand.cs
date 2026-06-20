namespace GoldMetrics.GoldCheck.Platform.MonitoringTelemetry.Domain.Model.Commands;

public record DetectPressureAnomalyCommand(string AssetId, string PressureType, decimal PressureBar);