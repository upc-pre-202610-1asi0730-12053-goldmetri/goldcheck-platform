namespace GoldMetrics.GoldCheck.Platform.FleetOperations.Domain.Model.ValueObjects;

public record LoadingPoint
{
    public LoadingPoint() => Name = string.Empty;

    public LoadingPoint(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("LoadingPoint name cannot be empty.", nameof(name));
        Name = name;
    }

    public string Name { get; init; }
}
