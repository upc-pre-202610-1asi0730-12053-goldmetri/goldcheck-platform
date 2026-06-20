namespace GoldMetrics.GoldCheck.Platform.MonitoringTelemetry.Domain.Model.Commands;

public record AnalysePressureCommand(string AssetId, string PressureType, decimal PressureBar);