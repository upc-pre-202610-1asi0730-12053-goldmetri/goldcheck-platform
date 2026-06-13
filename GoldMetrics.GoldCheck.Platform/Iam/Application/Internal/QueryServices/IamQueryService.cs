using GoldMetrics.GoldCheck.Platform.Iam.Application.QueryServices;
using GoldMetrics.GoldCheck.Platform.Iam.Domain.Model.Aggregates;
using GoldMetrics.GoldCheck.Platform.Iam.Domain.Model.Queries;
using GoldMetrics.GoldCheck.Platform.Iam.Domain.Repositories;
using GoldMetrics.GoldCheck.Platform.Iam.Resources;
using GoldMetrics.GoldCheck.Platform.Shared.Application.Model;
using Microsoft.Extensions.Localization;

namespace GoldMetrics.GoldCheck.Platform.Iam.Application.Internal.QueryServices;

public class IamQueryService(
    IUserRepository userRepository,
    IStringLocalizer<IamMessages> localizer) : IIamQueryService
{
    public Task<Result<User>> GetUserByIdAsync(GetUserByIdQuery query, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }

    public Task<Result<IEnumerable<User>>> GetAllUsersAsync(GetAllUsersQuery query, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }
}