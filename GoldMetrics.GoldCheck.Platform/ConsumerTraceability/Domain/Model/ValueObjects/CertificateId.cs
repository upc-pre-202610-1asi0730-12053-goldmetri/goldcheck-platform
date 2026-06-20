namespace GoldMetrics.GoldCheck.Platform.ConsumerTraceability.Domain.Model.ValueObjects;

public record CertificateId
{
    public CertificateId() => Value = string.Empty;

    public CertificateId(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("CertificateId cannot be empty.", nameof(value));
        Value = value;
    }

    public string Value { get; init; }
}