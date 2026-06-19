using GoldMetrics.GoldCheck.Platform.SubscriptionsAndBilling.Domain.Model.Commands;
using GoldMetrics.GoldCheck.Platform.SubscriptionsAndBilling.Domain.Model.Entities;
using GoldMetrics.GoldCheck.Platform.SubscriptionsAndBilling.Domain.Model.ValueObjects;

namespace GoldMetrics.GoldCheck.Platform.SubscriptionsAndBilling.Domain.Model.Aggregates;

public partial class UserSubscription
{
    public int Id { get; private set; }
    public UserId UserId { get; private set; } = new();
    public PlanType PlanType { get; private set; } = new();
    public BillingCycle BillingCycle { get; private set; } = new();
    public SubscriptionStatus SubscriptionStatus { get; private set; } = new();
    public List<string> AssignedFeatures { get; private set; } = [];
    public List<Invoice> Invoices { get; private set; } = [];
    public bool AccessGranted { get; private set; }
    public string? RequestedDowngradePlan { get; private set; }
    public string Status { get; private set; } = string.Empty;

    public UserSubscription() { }

    public UserSubscription(SelectPlanCommand command)
    {
        UserId = new UserId(command.UserId);
        PlanType = new PlanType(command.PlanType);
        BillingCycle = new BillingCycle(command.BillingCycle);
        SubscriptionStatus = new SubscriptionStatus("Active");
        AssignedFeatures = [];
        Invoices = [];
        Status = "PlanSelected";
    }
    
    public void ConfirmSubscription(ConfirmSubscriptionCommand command)
    {
        SubscriptionStatus = new SubscriptionStatus("Active");
        Status = "SubscriptionActivated";
    }
    
    public void RestrictFeatures(RestrictFeaturesCommand command)
    {
        SubscriptionStatus = new SubscriptionStatus("Restricted");
        Status = "FeaturesRestricted";
    }
}