using GoldMetrics.GoldCheck.Platform.Shared.Domain.Model.Events;

namespace GoldMetrics.GoldCheck.Platform.MonitoringTelemetry.Domain.Model.Events;

public record OPCCommunicationAnalysedEvent(string AssetId) : IEvent;