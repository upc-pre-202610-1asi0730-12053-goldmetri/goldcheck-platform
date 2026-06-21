namespace GoldMetrics.GoldCheck.Platform.Iam.Interfaces.Acl;

public interface IIamContextFacade
{
    Task<bool> ValidateUserExists(string userId, CancellationToken cancellationToken);
    Task<string> FetchUsernameByUserId(string userId, CancellationToken cancellationToken);
}
