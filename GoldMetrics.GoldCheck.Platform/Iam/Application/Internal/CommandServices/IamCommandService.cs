using GoldMetrics.GoldCheck.Platform.Iam.Application.CommandServices;
using GoldMetrics.GoldCheck.Platform.Iam.Domain.Model.Aggregates;
using GoldMetrics.GoldCheck.Platform.Iam.Domain.Model.Commands;
using GoldMetrics.GoldCheck.Platform.Iam.Resources;
using GoldMetrics.GoldCheck.Platform.Shared.Application.Model;
using GoldMetrics.GoldCheck.Platform.Shared.Domain.Repositories;
using GoldMetrics.GoldCheck.Platform.Iam.Domain.Repositories;
using Microsoft.Extensions.Localization;

namespace GoldMetrics.GoldCheck.Platform.Iam.Application.Internal.CommandServices;

public class IamCommandService(
    IUserRepository userRepository,
    IUnitOfWork unitOfWork,
    IStringLocalizer<IamMessages> localizer) : IIamCommandService
{
    public Task<Result<User>> RegisterUserAsync(RegisterUserCommand command, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }
}