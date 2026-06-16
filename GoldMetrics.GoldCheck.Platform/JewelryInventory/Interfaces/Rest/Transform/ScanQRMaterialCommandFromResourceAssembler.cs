using GoldMetrics.GoldCheck.Platform.JewelryInventory.Domain.Model.Commands;
using GoldMetrics.GoldCheck.Platform.JewelryInventory.Interfaces.Rest.Resources;

namespace GoldMetrics.GoldCheck.Platform.JewelryInventory.Interfaces.Rest.Transform;

public static class ScanQRMaterialCommandFromResourceAssembler
{
    public static ScanQRMaterialCommand ToCommandFromResource(
        string materialId, ScanQRResource resource)
        => new(materialId, resource.QRCode);
}
