using GoldMetrics.GoldCheck.Platform.Iam.Application.QueryServices;
using GoldMetrics.GoldCheck.Platform.Iam.Domain.Model.Aggregates;
using GoldMetrics.GoldCheck.Platform.Iam.Domain.Model.Errors;
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
    public async Task<Result<User>> GetUserByIdAsync(GetUserByIdQuery query, CancellationToken ct = default)
    {
        try
        {
            if (!int.TryParse(query.UserId, out var userId))
                return Result<User>.Failure(IamError.UserNotFound, localizer[nameof(IamError.UserNotFound)]);

            var user = await userRepository.FindByIdAsync(userId);
            return user is null
                ? Result<User>.Failure(IamError.UserNotFound, localizer[nameof(IamError.UserNotFound)])
                : Result<User>.Success(user);
        }
        catch (OperationCanceledException)
        {
            return Result<User>.Failure(IamError.OperationCancelled, localizer[nameof(IamError.OperationCancelled)]);
        }
        catch (Exception)
        {
            return Result<User>.Failure(IamError.InternalServerError, localizer[nameof(IamError.InternalServerError)]);
        }
    }

    public async Task<Result<IEnumerable<User>>> GetAllUsersAsync(GetAllUsersQuery query, CancellationToken ct = default)
    {
        try
        {
            var users = await userRepository.FindAllAsync();
            return Result<IEnumerable<User>>.Success(users);
        }
        catch (OperationCanceledException)
        {
            return Result<IEnumerable<User>>.Failure(IamError.OperationCancelled, localizer[nameof(IamError.OperationCancelled)]);
        }
        catch (Exception)
        {
            return Result<IEnumerable<User>>.Failure(IamError.InternalServerError, localizer[nameof(IamError.InternalServerError)]);
        }
    }
}