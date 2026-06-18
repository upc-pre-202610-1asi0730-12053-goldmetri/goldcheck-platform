namespace GoldMetrics.GoldCheck.Platform.Analytics.Domain.Model.ValueObjects;

public record ProductionPeriod
{
    public ProductionPeriod() { Start = DateTimeOffset.MinValue; End = DateTimeOffset.MinValue; }
    public ProductionPeriod(DateTimeOffset start, DateTimeOffset end)
    {
        if (start >= end)
            throw new ArgumentException("Production period Start must be earlier than End.");
        Start = start;
        End = end;
    }
    public DateTimeOffset Start { get; init; }
    public DateTimeOffset End { get; init; }
}