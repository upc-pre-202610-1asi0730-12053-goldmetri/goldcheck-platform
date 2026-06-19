namespace GoldMetrics.GoldCheck.Platform.SubscriptionsAndBilling.Domain.Model.Commands;
public record CheckFeatureAccessCommand(string UserId, string FeatureName);