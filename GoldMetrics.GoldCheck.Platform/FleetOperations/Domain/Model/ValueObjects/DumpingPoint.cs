namespace GoldMetrics.GoldCheck.Platform.FleetOperations.Domain.Model.ValueObjects;

public record DumpingPoint
{
    public DumpingPoint() => Name = string.Empty;

    public DumpingPoint(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("DumpingPoint name cannot be empty.", nameof(name));
        Name = name;
    }

    public string Name { get; init; }
}
