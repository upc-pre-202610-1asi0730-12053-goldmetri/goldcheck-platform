using GoldMetrics.GoldCheck.Platform.Shared.Domain.Model.Events;

namespace GoldMetrics.GoldCheck.Platform.MonitoringTelemetry.Domain.Model.Events;

public record TemperatureAnomalyDetectedEvent(string AssetId, string AnomalyType, decimal AnomalyCelsius) : IEvent;