using GoldMetrics.GoldCheck.Platform.MaterialOperations.Domain.Model.Commands;
using GoldMetrics.GoldCheck.Platform.MaterialOperations.Interfaces.Rest.Resources;

namespace GoldMetrics.GoldCheck.Platform.MaterialOperations.Interfaces.Rest.Transform;

public static class IdentifyMineralTypeCommandFromResourceAssembler
{
    public static IdentifyMineralTypeCommand ToCommandFromResource(CreateMaterialResource resource) =>
        new(resource.BatchId, resource.MineralType, resource.PayloadTons);
}
