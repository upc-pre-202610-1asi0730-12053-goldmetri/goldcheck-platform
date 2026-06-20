using GoldMetrics.GoldCheck.Platform.Shared.Domain.Model.Events;

namespace GoldMetrics.GoldCheck.Platform.MonitoringTelemetry.Domain.Model.Events;

public record PressureAnomalyDetectedEvent(string AssetId, string PressureType) : IEvent;