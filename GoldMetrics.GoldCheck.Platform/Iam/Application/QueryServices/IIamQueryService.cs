using GoldMetrics.GoldCheck.Platform.Iam.Domain.Model.Aggregates;
using GoldMetrics.GoldCheck.Platform.Iam.Domain.Model.Queries;
using GoldMetrics.GoldCheck.Platform.Shared.Application.Model;

namespace GoldMetrics.GoldCheck.Platform.Iam.Application.QueryServices;

public interface IIamQueryService
{
    Task<Result<User>> GetUserByIdAsync(GetUserByIdQuery query, CancellationToken ct = default);
    Task<Result<IEnumerable<User>>> GetAllUsersAsync(GetAllUsersQuery query, CancellationToken ct = default);
    Task<Result<User>> GetUserByUsernameAsync(GetUserByUsernameQuery query, CancellationToken ct = default);
}