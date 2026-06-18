namespace GoldMetrics.GoldCheck.Platform.AssetMaintenance.Domain.Model.Commands;

public record DischargeMachineryCommand(string MachineryId, string Reason);