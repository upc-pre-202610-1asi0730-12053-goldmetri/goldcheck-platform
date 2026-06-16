using GoldMetrics.GoldCheck.Platform.JewelryInventory.Domain.Model.Aggregates;
using GoldMetrics.GoldCheck.Platform.JewelryInventory.Interfaces.Rest.Resources;

namespace GoldMetrics.GoldCheck.Platform.JewelryInventory.Interfaces.Rest.Transform;

public static class JewelryMaterialResourceFromEntityAssembler
{
    public static JewelryMaterialResource ToResourceFromEntity(JewelryMaterial entity)
        => new(
            entity.Id,
            entity.MaterialId.Value,
            entity.JewelerId.Value,
            entity.Status.Value,
            entity.QRCodeValue);
}
