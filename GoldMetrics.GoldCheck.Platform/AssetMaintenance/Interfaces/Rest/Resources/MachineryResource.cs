namespace GoldMetrics.GoldCheck.Platform.AssetMaintenance.Interfaces.Rest.Resources;

public record MachineryResource(
    int Id,
    string MachineryId,
    string Model,
    string OEM,
    decimal EngineHours,
    string MaintenanceStatus,
    decimal? MaintenanceScheduledAtHours,
    string? DischargeReason,
    string Status);