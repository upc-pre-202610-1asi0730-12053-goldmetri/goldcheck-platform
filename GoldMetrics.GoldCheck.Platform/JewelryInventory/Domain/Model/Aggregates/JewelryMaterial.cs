using GoldMetrics.GoldCheck.Platform.JewelryInventory.Domain.Model.Commands;
using GoldMetrics.GoldCheck.Platform.JewelryInventory.Domain.Model.ValueObjects;

namespace GoldMetrics.GoldCheck.Platform.JewelryInventory.Domain.Model.Aggregates;

public partial class JewelryMaterial
{
    public JewelryMaterial()
    {
        MaterialId = new MaterialId();
        JewelerId = new JewelerId();
        Status = new MaterialStatus();
    }

    public JewelryMaterial(RegisterNonCertifiedMaterialCommand command)
    {
        MaterialId = new MaterialId(command.MaterialId);
        JewelerId = new JewelerId(command.JewelerId);
        Status = new MaterialStatus("NonCertified");
    }

    public int Id { get; }
    public MaterialId MaterialId { get; private set; }
    public JewelerId JewelerId { get; private set; }
    public MaterialStatus Status { get; private set; }

    public string? QRCodeValue { get; private set; }

    public void ScanQR(ScanQRMaterialCommand command)
    {
        var qr = new QRCode(command.QRCode);
        QRCodeValue = qr.Value;
        Status = new MaterialStatus("Pending");
    }

    public void RegisterInInventory(RegisterMaterialInInventoryCommand command)
    {
        var qr = new QRCode(command.QRCode);
        QRCodeValue = qr.Value;
        Status = new MaterialStatus("Pending");
    }
}
