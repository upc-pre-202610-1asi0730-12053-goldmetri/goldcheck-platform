using GoldMetrics.GoldCheck.Platform.JewelryInventory.Domain.Model.Aggregates;
using GoldMetrics.GoldCheck.Platform.JewelryInventory.Domain.Model.Commands;
using GoldMetrics.GoldCheck.Platform.Shared.Application.Model;

namespace GoldMetrics.GoldCheck.Platform.JewelryInventory.Application.CommandServices;

public interface IJewelryMaterialCommandService
{
    Task<Result<JewelryMaterial>> Handle(RegisterNonCertifiedMaterialCommand command,
        CancellationToken cancellationToken = default);
}
