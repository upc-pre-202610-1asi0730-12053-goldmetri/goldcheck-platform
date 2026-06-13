using GoldMetrics.GoldCheck.Platform.Iam.Application.CommandServices;
using GoldMetrics.GoldCheck.Platform.Iam.Domain.Model.Aggregates;
using GoldMetrics.GoldCheck.Platform.Iam.Domain.Model.Commands;
using GoldMetrics.GoldCheck.Platform.Iam.Domain.Model.Errors;
using GoldMetrics.GoldCheck.Platform.Iam.Domain.Repositories;
using GoldMetrics.GoldCheck.Platform.Iam.Resources;
using GoldMetrics.GoldCheck.Platform.Shared.Application.Model;
using GoldMetrics.GoldCheck.Platform.Shared.Domain.Repositories;
using Microsoft.Extensions.Localization;

namespace GoldMetrics.GoldCheck.Platform.Iam.Application.Internal.CommandServices;

public class IamCommandService(
    IUserRepository userRepository,
    IUnitOfWork unitOfWork,
    IStringLocalizer<IamMessages> localizer) : IIamCommandService
{
    public async Task<Result<User>> RegisterUserAsync(RegisterUserCommand command, CancellationToken ct = default)
    {
        try
        {
            var existingUsername = await userRepository.FindByUsernameAsync(command.Username);
            if (existingUsername is not null)
                return Result<User>.Failure(IamError.UsernameAlreadyExists, localizer[nameof(IamError.UsernameAlreadyExists)]);

            var existingEmail = await userRepository.FindByEmailAsync(command.Email);
            if (existingEmail is not null)
                return Result<User>.Failure(IamError.EmailAlreadyRegistered, localizer[nameof(IamError.EmailAlreadyRegistered)]);

            var user = new User(command);
            await userRepository.AddAsync(user);
            await unitOfWork.CompleteAsync(ct);
            return Result<User>.Success(user);
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
}