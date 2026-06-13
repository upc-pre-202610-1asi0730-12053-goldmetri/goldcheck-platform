using GoldMetrics.GoldCheck.Platform.Shared.Domain.Model.Events;

namespace GoldMetrics.GoldCheck.Platform.Iam.Domain.Model.Events;

public record UserLoggedInEvent(int UserId, string Username) : IEvent;

