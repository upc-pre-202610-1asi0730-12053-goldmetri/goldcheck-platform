namespace GoldMetrics.GoldCheck.Platform.AssetMaintenance.Domain.Model.Commands;

public record RegisterMachineryCommand(string MachineryId, string Model, string OEM);