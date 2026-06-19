namespace GoldMetrics.GoldCheck.Platform.AssetMaintenance.Domain.Model.Commands;

public record DischargeComponentCommand(string MachineryId, string ComponentId, string Reason);