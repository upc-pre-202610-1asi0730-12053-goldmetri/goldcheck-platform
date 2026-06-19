namespace GoldMetrics.GoldCheck.Platform.SubscriptionsAndBilling.Domain.Model.Commands;
public record ConfirmSubscriptionCommand(string UserId, string PaymentMethod);