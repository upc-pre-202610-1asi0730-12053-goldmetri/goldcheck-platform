namespace GoldMetrics.GoldCheck.Platform.MaterialOperations.Domain.Model.ValueObjects;

public record MaterialBatchId
{
    public MaterialBatchId() => Value = string.Empty;

    public MaterialBatchId(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("MaterialBatchId cannot be empty.", nameof(value));
        Value = value;
    }

    public string Value { get; init; }
}
