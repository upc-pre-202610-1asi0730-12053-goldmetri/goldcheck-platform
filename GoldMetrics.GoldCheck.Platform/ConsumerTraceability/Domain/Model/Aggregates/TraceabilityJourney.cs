using GoldMetrics.GoldCheck.Platform.ConsumerTraceability.Domain.Model.Commands;
using GoldMetrics.GoldCheck.Platform.ConsumerTraceability.Domain.Model.ValueObjects;

namespace GoldMetrics.GoldCheck.Platform.ConsumerTraceability.Domain.Model.Aggregates;

public partial class TraceabilityJourney
{
    public TraceabilityJourney()
    {
        ProductQRCode = new ProductQRCode();
        ConsumerId = new ConsumerId();
        JourneySummary = string.Empty;
        Status = string.Empty;
    }

    /// <summary>Created automatically when a product QR is scanned.</summary>
    public TraceabilityJourney(ScanProductQRCommand command)
    {
        ProductQRCode = new ProductQRCode(command.QRCode);
        ConsumerId = new ConsumerId(command.ConsumerId);
        JourneySummary = $"Initial scan of product {command.QRCode} by consumer {command.ConsumerId}.";
        Status = "Initiated";
    }

    public int Id { get; }
    public ProductQRCode ProductQRCode { get; private set; }
    public ConsumerId ConsumerId { get; private set; }
    public string JourneySummary { get; private set; }
    public string Status { get; private set; }
}