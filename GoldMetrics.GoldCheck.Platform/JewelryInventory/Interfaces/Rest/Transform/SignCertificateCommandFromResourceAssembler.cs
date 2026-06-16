using GoldMetrics.GoldCheck.Platform.JewelryInventory.Domain.Model.Commands;
using GoldMetrics.GoldCheck.Platform.JewelryInventory.Interfaces.Rest.Resources;

namespace GoldMetrics.GoldCheck.Platform.JewelryInventory.Interfaces.Rest.Transform;

public static class SignCertificateCommandFromResourceAssembler
{
    public static SignCertificateCommand ToCommandFromResource(
        string certificateId, SignCertificateResource resource)
        => new(certificateId, resource.JewelerId);
}
