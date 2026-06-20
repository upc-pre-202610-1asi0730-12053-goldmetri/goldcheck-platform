using GoldMetrics.GoldCheck.Platform.ConsumerTraceability.Domain.Model.Commands;
using GoldMetrics.GoldCheck.Platform.ConsumerTraceability.Interfaces.Rest.Resources;

namespace GoldMetrics.GoldCheck.Platform.ConsumerTraceability.Interfaces.Rest.Transform;

public static class ScanProductQRCommandFromResourceAssembler
{
    public static ScanProductQRCommand ToCommandFromResource(ScanProductQRResource resource)
        => new(resource.QRCode, resource.ConsumerId);
}