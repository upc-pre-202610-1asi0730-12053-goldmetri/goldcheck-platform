namespace GoldMetrics.GoldCheck.Platform.Iam.Domain.Model.ValueObjects;

public record Email
{
    public Email() => Address = string.Empty;
    public Email(string address)
    {
        if (string.IsNullOrWhiteSpace(address))
            throw new ArgumentException("Email cannot be empty.", nameof(address));
        if (!address.Contains('@'))
            throw new ArgumentException("Email must contain a valid @ character.", nameof(address));
        Address = address;
    }
    public string Address { get; init; }
}

