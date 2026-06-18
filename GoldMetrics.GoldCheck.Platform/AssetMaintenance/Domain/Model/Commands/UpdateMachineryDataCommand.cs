namespace GoldMetrics.GoldCheck.Platform.AssetMaintenance.Domain.Model.Commands;

public record UpdateMachineryDataCommand(string MachineryId, decimal CurrentEngineHours);