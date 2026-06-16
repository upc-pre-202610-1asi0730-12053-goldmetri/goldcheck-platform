namespace GoldMetrics.GoldCheck.Platform.JewelryInventory.Domain.Model.ValueObjects;

public record MaterialStatus
{
    private static readonly HashSet<string> AllowedStatuses =
        new(StringComparer.OrdinalIgnoreCase) { "NonCertified", "Certified", "Pending" };

    public MaterialStatus() => Value = string.Empty;

    public MaterialStatus(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("MaterialStatus cannot be empty.", nameof(value));
        if (!AllowedStatuses.Contains(value))
            throw new ArgumentException(
                $"Invalid material status '{value}'. Allowed: NonCertified.",
                nameof(value));
        Value = value;
    }

    public string Value { get; init; }
}
