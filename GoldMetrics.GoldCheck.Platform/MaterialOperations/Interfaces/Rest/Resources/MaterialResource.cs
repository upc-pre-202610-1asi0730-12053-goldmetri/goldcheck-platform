namespace GoldMetrics.GoldCheck.Platform.MaterialOperations.Interfaces.Rest.Resources;

public record MaterialResource(
    int Id,
    string BatchId,
    string MineralType,
    decimal PayloadTons,
    string Status);
