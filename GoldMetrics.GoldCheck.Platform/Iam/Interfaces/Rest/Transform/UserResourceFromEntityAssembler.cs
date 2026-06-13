using GoldMetrics.GoldCheck.Platform.Iam.Domain.Model.Aggregates;
using GoldMetrics.GoldCheck.Platform.Iam.Interfaces.Rest.Resources;

namespace GoldMetrics.GoldCheck.Platform.Iam.Interfaces.Rest.Transform;

public static class UserResourceFromEntityAssembler
{
    public static UserResponse ToResourceFromEntity(User user) =>
        new(user.Id, user.Username.Value, user.Email.Address, user.Role.Value, user.Status);
}

