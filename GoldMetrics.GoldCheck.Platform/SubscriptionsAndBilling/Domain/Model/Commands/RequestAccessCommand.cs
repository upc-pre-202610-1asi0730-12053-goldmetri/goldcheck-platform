namespace GoldMetrics.GoldCheck.Platform.SubscriptionsAndBilling.Domain.Model.Commands;
public record RequestAccessCommand(string UserId, string FeatureName);