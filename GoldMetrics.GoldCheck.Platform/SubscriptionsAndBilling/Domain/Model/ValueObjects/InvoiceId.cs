namespace GoldMetrics.GoldCheck.Platform.SubscriptionsAndBilling.Domain.Model.ValueObjects;
public record InvoiceId
{
    public InvoiceId() => Value = string.Empty;
    public InvoiceId(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("InvoiceId cannot be empty.", nameof(value));
        Value = value;
    }
    public string Value { get; init; }
}