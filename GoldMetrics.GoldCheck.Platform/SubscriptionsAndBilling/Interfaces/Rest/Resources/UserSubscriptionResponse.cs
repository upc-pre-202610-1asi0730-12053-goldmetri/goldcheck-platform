namespace GoldMetrics.GoldCheck.Platform.SubscriptionsAndBilling.Interfaces.Rest.Resources;
public record UserSubscriptionResponse(int Id, string UserId, string PlanType, string BillingCycle,
    string SubscriptionStatus, List<string> AssignedFeatures, bool AccessGranted, string Status);