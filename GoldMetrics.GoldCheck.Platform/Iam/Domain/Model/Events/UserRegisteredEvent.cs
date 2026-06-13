using GoldMetrics.GoldCheck.Platform.Shared.Domain.Model.Events;

namespace GoldMetrics.GoldCheck.Platform.Iam.Domain.Model.Events;

public record UserRegisteredEvent(int UserId, string Username, string Email) : IEvent;

