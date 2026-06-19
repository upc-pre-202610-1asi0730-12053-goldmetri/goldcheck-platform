namespace GoldMetrics.GoldCheck.Platform.SubscriptionsAndBilling.Domain.Model.ValueObjects;
public record AdministratorId
{
    public AdministratorId() => Value = string.Empty;
    public AdministratorId(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("AdministratorId cannot be empty.", nameof(value));
        Value = value;
    }
    public string Value { get; init; }
}