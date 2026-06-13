using GoldMetrics.GoldCheck.Platform.Iam.Application.CommandServices;
using GoldMetrics.GoldCheck.Platform.Iam.Domain.Model.Aggregates;
using GoldMetrics.GoldCheck.Platform.Iam.Domain.Model.Commands;
using GoldMetrics.GoldCheck.Platform.Iam.Domain.Model.Errors;
using GoldMetrics.GoldCheck.Platform.Iam.Domain.Repositories;
using GoldMetrics.GoldCheck.Platform.Iam.Resources;
using GoldMetrics.GoldCheck.Platform.Shared.Application.Model;
using GoldMetrics.GoldCheck.Platform.Shared.Domain.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace GoldMetrics.GoldCheck.Platform.Iam.Application.Internal.CommandServices;

public class IamCommandService(
    IUserRepository userRepository,
    IUnitOfWork unitOfWork,
    IStringLocalizer<IamMessages> localizer,
    IConfiguration configuration) : IIamCommandService
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
        catch (OperationCanceledException) { return Cancelled<User>(); }
        catch (Exception) { return ServerError<User>(); }
    }

    public async Task<Result<string>> AuthenticateUserAsync(AuthenticateUserCommand command, CancellationToken ct = default)
    {
        try
        {
            var user = await userRepository.FindByUsernameAsync(command.Username);
            if (user is null) return Result<string>.Failure(IamError.UserNotFound, localizer[nameof(IamError.UserNotFound)]);

            if (!BCrypt.Net.BCrypt.Verify(command.Password, user.HashedPassword.Value))
                return Result<string>.Failure(IamError.InvalidCredentials, localizer[nameof(IamError.InvalidCredentials)]);

            var token = GenerateJwtToken(user);
            return Result<string>.Success(token);
        }
        catch (OperationCanceledException) { return Cancelled<string>(); }
        catch (Exception) { return ServerError<string>(); }
    }

    public async Task<Result<User>> UpdateProfileAsync(UpdateProfileCommand command, CancellationToken ct = default)
    {
        try
        {
            if (!int.TryParse(command.UserId, out var userId))
                return NotFound<User>();

            var user = await userRepository.FindByIdAsync(userId);
            if (user is null) return NotFound<User>();

            user.UpdateProfile(command);
            userRepository.Update(user);
            await unitOfWork.CompleteAsync(ct);
            return Result<User>.Success(user);
        }
        catch (OperationCanceledException) { return Cancelled<User>(); }
        catch (Exception) { return ServerError<User>(); }
    }

    private string GenerateJwtToken(User user)
    {
        var secret = configuration["TokenSettings:Secret"] ?? string.Empty;
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.UniqueName, user.Username.Value),
            new Claim(ClaimTypes.Role, user.Role.Value),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };
        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.UtcNow.AddHours(8),
            signingCredentials: credentials);
        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private Result<T> NotFound<T>() =>
        Result<T>.Failure(IamError.UserNotFound, localizer[nameof(IamError.UserNotFound)]);
    private Result<T> Cancelled<T>() =>
        Result<T>.Failure(IamError.OperationCancelled, localizer[nameof(IamError.OperationCancelled)]);
    private Result<T> DbError<T>() =>
        Result<T>.Failure(IamError.DatabaseError, localizer[nameof(IamError.DatabaseError)]);
    private Result<T> ServerError<T>() =>
        Result<T>.Failure(IamError.InternalServerError, localizer[nameof(IamError.InternalServerError)]);
}

