namespace GoldMetrics.GoldCheck.Platform.ConsumerTraceability.Domain.Model.Commands;

public record DownloadCertificateCommand(string CertificateId, string ConsumerId);