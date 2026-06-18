namespace GoldMetrics.GoldCheck.Platform.AssetMaintenance.Domain.Model.Commands;

public record SchedulePreventiveMaintenanceCommand(string MachineryId, decimal EngineHours);