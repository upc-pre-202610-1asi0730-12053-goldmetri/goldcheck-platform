using GoldMetrics.GoldCheck.Platform.MaterialOperations.Domain.Model.Aggregates;
using GoldMetrics.GoldCheck.Platform.MaterialOperations.Domain.Model.Commands;
using GoldMetrics.GoldCheck.Platform.Shared.Application.Model;

namespace GoldMetrics.GoldCheck.Platform.MaterialOperations.Application.CommandServices;

public interface IMaterialCommandService
{
    Task<Result<Material>> Handle(IdentifyMineralTypeCommand command, CancellationToken cancellationToken);
    Task<Result<Material>> Handle(ClassifyMaterialCommand command, CancellationToken cancellationToken);
}
