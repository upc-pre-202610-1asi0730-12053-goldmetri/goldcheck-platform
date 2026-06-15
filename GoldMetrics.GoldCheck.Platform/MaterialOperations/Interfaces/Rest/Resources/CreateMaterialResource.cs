namespace GoldMetrics.GoldCheck.Platform.MaterialOperations.Interfaces.Rest.Resources;

public record CreateMaterialResource(string BatchId, string MineralType, decimal PayloadTons);
