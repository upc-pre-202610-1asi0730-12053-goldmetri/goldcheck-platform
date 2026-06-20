using GoldMetrics.GoldCheck.Platform.Shared.Domain.Model.Events;

namespace GoldMetrics.GoldCheck.Platform.MonitoringTelemetry.Domain.Model.Events;

public record CommunicationAnomalyDetectedEvent(string AssetId, string Protocol) : IEvent;