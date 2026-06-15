namespace GoldMetrics.GoldCheck.Platform.MaterialOperations.Domain.Model.ValueObjects;

public record MineralType
{
    private static readonly HashSet<string> AllowedTypes =
        new(StringComparer.OrdinalIgnoreCase) { "Gold", "Silver", "Copper" };

    public MineralType() => Value = string.Empty;

    public MineralType(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("MineralType cannot be empty.", nameof(value));
        if (!AllowedTypes.Contains(value))
            throw new ArgumentException(
                $"Invalid mineral type '{value}'. Allowed values: Gold, Silver, Copper.", nameof(value));
        Value = value;
    }

    public string Value { get; init; }
}
