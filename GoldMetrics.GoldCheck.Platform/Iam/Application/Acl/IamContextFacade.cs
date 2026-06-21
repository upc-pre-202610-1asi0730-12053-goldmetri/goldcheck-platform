using GoldMetrics.GoldCheck.Platform.Iam.Application.QueryServices;
using GoldMetrics.GoldCheck.Platform.Iam.Domain.Model.Queries;
using GoldMetrics.GoldCheck.Platform.Iam.Interfaces.Acl;

namespace GoldMetrics.GoldCheck.Platform.Iam.Application.Acl;

public class IamContextFacade(IIamQueryService iamQueryService) : IIamContextFacade
{
    public async Task<bool> ValidateUserExists(string userId, CancellationToken cancellationToken)
    {
        var query = new GetUserByIdQuery(userId);
        var result = await iamQueryService.GetUserByIdAsync(query, cancellationToken);
        return result.IsSuccess && result.Value is not null;
    }

    public async Task<string> FetchUsernameByUserId(string userId, CancellationToken cancellationToken)
    {
        var query = new GetUserByIdQuery(userId);
        var result = await iamQueryService.GetUserByIdAsync(query, cancellationToken);
        return result.Value?.Username.Value ?? string.Empty;
    }
}
