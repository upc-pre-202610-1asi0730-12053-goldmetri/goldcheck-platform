using GoldMetrics.GoldCheck.Platform.ConsumerTraceability.Domain.Model.Commands;
using GoldMetrics.GoldCheck.Platform.ConsumerTraceability.Domain.Model.ValueObjects;

namespace GoldMetrics.GoldCheck.Platform.ConsumerTraceability.Domain.Model.Aggregates;

public partial class JewelryProduct
{
    public JewelryProduct()
    {
        QRCode = new ProductQRCode();
        ConsumerId = new ConsumerId();
        Status = string.Empty;
    }
    /// <summary>
    /// Created when a consumer downloads a certificate directly by its ID
    /// (the certificate ID is used as the product identifier in this context).
    /// </summary>
    public JewelryProduct(DownloadCertificateCommand command)
    {
        QRCode = new ProductQRCode(command.CertificateId);
        ConsumerId = new ConsumerId(command.ConsumerId);
        CertificateIdRef = command.CertificateId;
        Status = "CertificateDownloaded";
        ScanCount = 0;
    }

    /// <summary>Created when a consumer scans a product QR code for the first time.</summary>
    public JewelryProduct(ScanProductQRCommand command)
    {
        QRCode = new ProductQRCode(command.QRCode);
        ConsumerId = new ConsumerId(command.ConsumerId);
        Status = "Scanned";
        ScanCount = 1;
    }

    public int Id { get; }
    public ProductQRCode QRCode { get; private set; }
    public ConsumerId ConsumerId { get; private set; }
    public string Status { get; private set; }
    public int ScanCount { get; private set; }
    /// <summary>Populated when the product is linked to a certificate via download.</summary>
    public string? CertificateIdRef { get; private set; }

    public void RecordScan(ScanProductQRCommand command)
    {
        ConsumerId = new ConsumerId(command.ConsumerId);
        ScanCount++;
        Status = "Scanned";
    }
    public void DownloadCertificate(DownloadCertificateCommand command)
    {
        var cert = new CertificateId(command.CertificateId);
        CertificateIdRef = cert.Value;
        ConsumerId = new ConsumerId(command.ConsumerId);
        Status = "CertificateDownloaded";
    }
}