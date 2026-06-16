using GoldMetrics.GoldCheck.Platform.JewelryInventory.Domain.Model.Commands;
using GoldMetrics.GoldCheck.Platform.JewelryInventory.Interfaces.Rest.Resources;

namespace GoldMetrics.GoldCheck.Platform.JewelryInventory.Interfaces.Rest.Transform;

public static class GenerateCertificateCommandFromResourceAssembler
{
    public static GenerateCertificateCommand ToCommandFromResource(
        GenerateCertificateResource resource)
        => new(resource.MaterialId);
}
