namespace GoldMetrics.GoldCheck.Platform.MaterialOperations.Interfaces.Acl;

public interface IMaterialOperationsContextFacade
{
    Task<bool> ValidateMaterialExists(string batchId, CancellationToken cancellationToken);
    Task<string> FetchMaterialClassificationByBatchId(string batchId, CancellationToken cancellationToken);
}
