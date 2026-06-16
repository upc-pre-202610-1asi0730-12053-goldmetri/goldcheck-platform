using GoldMetrics.GoldCheck.Platform.JewelryInventory.Domain.Model.Aggregates;
using GoldMetrics.GoldCheck.Platform.JewelryInventory.Domain.Model.Commands;
using GoldMetrics.GoldCheck.Platform.Shared.Application.Model;

namespace GoldMetrics.GoldCheck.Platform.JewelryInventory.Application.CommandServices;

public interface IJewelryCommandService
{
    Task<Result<Jewelry>> Handle(SignCertificateCommand command,
        CancellationToken cancellationToken = default);
}
