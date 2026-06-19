namespace GoldMetrics.GoldCheck.Platform.SubscriptionsAndBilling.Domain.Model.Commands;
public record AssignFeaturesCommand(string UserId, string PlanType, string[] Features);