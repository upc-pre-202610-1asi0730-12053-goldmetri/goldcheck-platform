using GoldMetrics.GoldCheck.Platform.JewelryInventory.Domain.Model.Commands;
using GoldMetrics.GoldCheck.Platform.JewelryInventory.Interfaces.Rest.Resources;

namespace GoldMetrics.GoldCheck.Platform.JewelryInventory.Interfaces.Rest.Transform;

public static class RegisterNonCertifiedMaterialCommandFromResourceAssembler
{
    public static RegisterNonCertifiedMaterialCommand ToCommandFromResource(
        CreateMaterialResource resource)
        => new(resource.MaterialId, resource.JewelerId);
}
