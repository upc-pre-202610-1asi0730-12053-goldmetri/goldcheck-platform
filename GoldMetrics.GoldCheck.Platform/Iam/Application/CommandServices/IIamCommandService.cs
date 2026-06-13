using GoldMetrics.GoldCheck.Platform.Iam.Domain.Model.Aggregates;
using GoldMetrics.GoldCheck.Platform.Iam.Domain.Model.Commands;
using GoldMetrics.GoldCheck.Platform.Shared.Application.Model;

namespace GoldMetrics.GoldCheck.Platform.Iam.Application.CommandServices;

public interface IIamCommandService
{
    Task<Result<User>> RegisterUserAsync(RegisterUserCommand command, CancellationToken ct = default);
    Task<Result<string>> AuthenticateUserAsync(AuthenticateUserCommand command, CancellationToken ct = default);
    Task<Result<User>> UpdateProfileAsync(UpdateProfileCommand command, CancellationToken ct = default);
}