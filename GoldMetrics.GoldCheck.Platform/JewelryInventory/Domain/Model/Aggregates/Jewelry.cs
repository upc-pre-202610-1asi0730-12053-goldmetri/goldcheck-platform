using GoldMetrics.GoldCheck.Platform.JewelryInventory.Domain.Model.Commands;
using GoldMetrics.GoldCheck.Platform.JewelryInventory.Domain.Model.ValueObjects;

namespace GoldMetrics.GoldCheck.Platform.JewelryInventory.Domain.Model.Aggregates;

public partial class Jewelry
{
    public Jewelry()
    {
        CertificateId = new CertificateId();
        JewelerId = new JewelerId();
        MaterialIdRef = string.Empty;
    }

    public Jewelry(SignCertificateCommand command, string materialIdRef)
    {
        CertificateId = new CertificateId(command.CertificateId);
        JewelerId = new JewelerId(command.JewelerId);
        MaterialIdRef = materialIdRef;
        IsSigned = true;
    }

    public int Id { get; }
    public CertificateId CertificateId { get; private set; }
    public JewelerId JewelerId { get; private set; }

    public string MaterialIdRef { get; private set; }

    public bool IsSigned { get; private set; }
}
