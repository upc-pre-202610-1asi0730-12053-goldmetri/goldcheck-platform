using GoldMetrics.GoldCheck.Platform.Shared.Domain.Model.Events;

namespace GoldMetrics.GoldCheck.Platform.MonitoringTelemetry.Domain.Model.Events;

public record ExhaustTemperatureLimitPerCylinderAnalysedEvent(string AssetId, decimal LimitCelsius, int CylinderNumber) : IEvent;