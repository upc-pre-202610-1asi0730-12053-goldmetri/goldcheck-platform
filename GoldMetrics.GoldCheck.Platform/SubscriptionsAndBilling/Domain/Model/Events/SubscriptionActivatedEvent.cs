using GoldMetrics.GoldCheck.Platform.Shared.Domain.Model.Events;
namespace GoldMetrics.GoldCheck.Platform.SubscriptionsAndBilling.Domain.Model.Events;
public record SubscriptionActivatedEvent(string UserId, string PlanType) : IEvent;