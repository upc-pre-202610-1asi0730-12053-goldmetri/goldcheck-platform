namespace GoldMetrics.GoldCheck.Platform.MaterialOperations.Domain.Model.Commands;

public record IdentifyMineralTypeCommand(string BatchId, string MineralType, decimal PayloadTons);
