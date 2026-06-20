using GoldMetrics.GoldCheck.Platform.Shared.Domain.Model.Events;

namespace GoldMetrics.GoldCheck.Platform.ConsumerTraceability.Domain.Model.Events;

public record CertificateDownloadedEvent(string CertificateId, string ConsumerId) : IEvent;