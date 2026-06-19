using GoldMetrics.GoldCheck.Platform.SubscriptionsAndBilling.Domain.Model.ValueObjects;

namespace GoldMetrics.GoldCheck.Platform.SubscriptionsAndBilling.Domain.Model.Entities;

public class Invoice
{
    public int Id { get; private set; }
    public InvoiceId InvoiceId { get; private set; } = new();
    public Amount Amount { get; private set; } = new();
    public string Status { get; private set; } = string.Empty;
    public int UserSubscriptionId { get; private set; }

    public Invoice() { }

    public Invoice(string invoiceId, decimal amount)
    {
        InvoiceId = new InvoiceId(invoiceId);
        Amount = new Amount(amount);
        Status = "Generated";
    }

    public void MarkDownloaded() => Status = "Downloaded";
}