using GoldMetrics.GoldCheck.Platform.MaterialOperations.Application.QueryServices;
using GoldMetrics.GoldCheck.Platform.MaterialOperations.Domain.Model.Queries;
using GoldMetrics.GoldCheck.Platform.MaterialOperations.Interfaces.Acl;

namespace GoldMetrics.GoldCheck.Platform.MaterialOperations.Application.Acl;

public class MaterialOperationsContextFacade(IMaterialQueryService materialQueryService)
    : IMaterialOperationsContextFacade
{
    public async Task<bool> ValidateMaterialExists(string batchId, CancellationToken cancellationToken)
    {
        var query = new GetMaterialByIdQuery(batchId);
        var result = await materialQueryService.Handle(query, cancellationToken);
        return result is not null;
    }

    public async Task<string> FetchMaterialClassificationByBatchId(string batchId, CancellationToken cancellationToken)
    {
        var query = new GetMaterialByIdQuery(batchId);
        var result = await materialQueryService.Handle(query, cancellationToken);
        return result?.Classification ?? string.Empty;
    }
}
