using GoldMetrics.GoldCheck.Platform.Shared.Domain.Model.Events;

namespace GoldMetrics.GoldCheck.Platform.ConsumerTraceability.Domain.Model.Events;

public record ProductQRScannedEvent(string QRCode, string ConsumerId) : IEvent;