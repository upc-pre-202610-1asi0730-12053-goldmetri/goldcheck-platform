using System.Text.RegularExpressions;

namespace GoldMetrics.GoldCheck.Platform.JewelryInventory.Domain.Model.ValueObjects;

public record QRCode
{
    private static readonly Regex ValidFormat =
        new(@"^[A-Za-z0-9\-_]{6,100}$", RegexOptions.Compiled);

    public QRCode() => Value = string.Empty;

    public QRCode(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("QRCode cannot be empty.", nameof(value));
        if (!ValidFormat.IsMatch(value))
            throw new ArgumentException(
                "QRCode has an invalid format. Must be 6–100 alphanumeric characters, hyphens, or underscores.",
                nameof(value));
        Value = value;
    }

    public string Value { get; init; }
}
