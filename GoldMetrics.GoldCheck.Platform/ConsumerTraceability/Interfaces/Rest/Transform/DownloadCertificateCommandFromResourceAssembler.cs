using GoldMetrics.GoldCheck.Platform.ConsumerTraceability.Domain.Model.Commands;
using GoldMetrics.GoldCheck.Platform.ConsumerTraceability.Interfaces.Rest.Resources;

namespace GoldMetrics.GoldCheck.Platform.ConsumerTraceability.Interfaces.Rest.Transform;

public static class DownloadCertificateCommandFromResourceAssembler
{
    public static DownloadCertificateCommand ToCommandFromResource(
        string certificateId, DownloadCertificateResource resource)
        => new(certificateId, resource.ConsumerId);
}