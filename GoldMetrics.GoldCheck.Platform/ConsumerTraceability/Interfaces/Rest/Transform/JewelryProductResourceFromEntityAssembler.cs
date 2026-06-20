using GoldMetrics.GoldCheck.Platform.ConsumerTraceability.Domain.Model.Aggregates;
using GoldMetrics.GoldCheck.Platform.ConsumerTraceability.Interfaces.Rest.Resources;

namespace GoldMetrics.GoldCheck.Platform.ConsumerTraceability.Interfaces.Rest.Transform;

public static class JewelryProductResourceFromEntityAssembler
{
    public static JewelryProductResource ToResourceFromEntity(JewelryProduct entity)
        => new(
            entity.Id,
            entity.QRCode.Value,
            entity.ConsumerId.Value,
            entity.CertificateIdRef,
            entity.Status,
            entity.ScanCount);
}