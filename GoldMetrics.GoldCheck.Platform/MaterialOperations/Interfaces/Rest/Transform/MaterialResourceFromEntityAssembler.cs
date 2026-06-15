using GoldMetrics.GoldCheck.Platform.MaterialOperations.Domain.Model.Aggregates;
using GoldMetrics.GoldCheck.Platform.MaterialOperations.Interfaces.Rest.Resources;

namespace GoldMetrics.GoldCheck.Platform.MaterialOperations.Interfaces.Rest.Transform;

public static class MaterialResourceFromEntityAssembler
{
    public static MaterialResource ToResourceFromEntity(Material entity) =>
        new(
            entity.Id,
            entity.BatchId.Value,
            entity.MineralType.Value,
            entity.Payload.Tons,
            entity.Classification,
            entity.DumpingPointName,
            entity.CurrentLocation,
            entity.Status);
}
