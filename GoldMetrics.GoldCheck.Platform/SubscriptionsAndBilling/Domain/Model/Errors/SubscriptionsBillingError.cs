namespace GoldMetrics.GoldCheck.Platform.SubscriptionsAndBilling.Domain.Model.Errors;

public enum SubscriptionsBillingError
{
    None,
    UserSubscriptionNotFound,
    InvoiceNotFound,
    InvalidPlanType,
    InvalidBillingCycle,
    PaymentMethodValidationFailed,
    FeatureNotAvailableInPlan,
    DowngradeNotAllowed,
    AccessDenied,
    OperationCancelled,
    DatabaseError,
    InternalServerError
}