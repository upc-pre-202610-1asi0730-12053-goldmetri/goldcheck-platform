using GoldMetrics.GoldCheck.Platform.JewelryInventory.Domain.Model.Aggregates;
using GoldMetrics.GoldCheck.Platform.JewelryInventory.Interfaces.Rest.Resources;

namespace GoldMetrics.GoldCheck.Platform.JewelryInventory.Interfaces.Rest.Transform;

public static class CertificateResourceFromEntityAssembler
{
    public static CertificateResource ToResourceFromEntity(Jewelry entity)
        => new(
            entity.Id,
            entity.CertificateId.Value,
            entity.MaterialIdRef,
            entity.JewelerId.Value,
            entity.IsSigned);
}
