namespace GoldMetrics.GoldCheck.Platform.SubscriptionsAndBilling.Domain.Model.Commands;
public record SelectPlanCommand(string UserId, string PlanType, string BillingCycle);