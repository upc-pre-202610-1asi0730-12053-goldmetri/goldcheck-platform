using GoldMetrics.GoldCheck.Platform.SubscriptionsAndBilling.Domain.Model.Aggregates;
using GoldMetrics.GoldCheck.Platform.SubscriptionsAndBilling.Domain.Model.Entities;
using GoldMetrics.GoldCheck.Platform.SubscriptionsAndBilling.Interfaces.Rest.Resources;

namespace GoldMetrics.GoldCheck.Platform.SubscriptionsAndBilling.Interfaces.Rest.Transform;

public static class UserSubscriptionResourceFromEntityAssembler
{
    public static UserSubscriptionResponse ToResponseFromEntity(UserSubscription entity) =>
        new(entity.Id, entity.UserId.Value, entity.PlanType.Value, entity.BillingCycle.Value,
            entity.SubscriptionStatus.Value, entity.AssignedFeatures, entity.AccessGranted, entity.Status);

    public static InvoiceResponse ToInvoiceResponseFromEntity(Invoice invoice) =>
        new(invoice.Id, invoice.InvoiceId.Value, invoice.Amount.Value, invoice.Status);
}