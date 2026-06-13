namespace GoldMetrics.GoldCheck.Platform.ReportingNotifications.Domain.Model.ValueObjects;

public record SupervisorId
{
    public SupervisorId() => Value = string.Empty;
    public SupervisorId(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("SupervisorId cannot be empty.", nameof(value));
        Value = value;
    }
    public string Value { get; init; }
}