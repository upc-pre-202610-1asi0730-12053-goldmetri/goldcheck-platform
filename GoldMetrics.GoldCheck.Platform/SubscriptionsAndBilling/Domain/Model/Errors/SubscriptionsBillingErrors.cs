namespace GoldMetrics.GoldCheck.Platform.SubscriptionsAndBilling.Domain.Model.Errors;

public static class SubscriptionsBillingErrors
{
    public static string KeyFor(SubscriptionsBillingError error) => $"SubscriptionsBillingError.{error}";
}