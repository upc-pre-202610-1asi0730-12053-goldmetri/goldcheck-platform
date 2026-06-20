using GoldMetrics.GoldCheck.Platform.Shared.Domain.Model.Events;

namespace GoldMetrics.GoldCheck.Platform.MonitoringTelemetry.Domain.Model.Events;

public record PressureAnomalyLoggedEvent(string AssetId, string AnomalyDescription) : IEvent;