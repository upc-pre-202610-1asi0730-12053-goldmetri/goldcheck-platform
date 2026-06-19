namespace GoldMetrics.GoldCheck.Platform.SubscriptionsAndBilling.Domain.Model.Commands;
public record RequestDowngradeCommand(string UserId, string NewPlanType);