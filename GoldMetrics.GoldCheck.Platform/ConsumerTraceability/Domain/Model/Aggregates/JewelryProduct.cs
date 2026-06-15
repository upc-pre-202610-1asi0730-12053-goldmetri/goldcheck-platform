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

    public void RecordScan(ScanProductQRCommand command)
    {
        ConsumerId = new ConsumerId(command.ConsumerId);
        ScanCount++;
        Status = "Scanned";
    }
}