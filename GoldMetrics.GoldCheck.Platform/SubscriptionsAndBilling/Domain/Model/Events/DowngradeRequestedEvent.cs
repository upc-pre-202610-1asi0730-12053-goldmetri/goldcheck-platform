using GoldMetrics.GoldCheck.Platform.Shared.Domain.Model.Events;
namespace GoldMetrics.GoldCheck.Platform.SubscriptionsAndBilling.Domain.Model.Events;
public record DowngradeRequestedEvent(string UserId, string NewPlanType) : IEvent;