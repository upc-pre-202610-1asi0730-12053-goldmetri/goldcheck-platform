namespace GoldMetrics.GoldCheck.Platform.MaterialOperations.Domain.Model.Commands;

public record TrackMaterialMovementCommand(string BatchId, string CurrentLocation);
