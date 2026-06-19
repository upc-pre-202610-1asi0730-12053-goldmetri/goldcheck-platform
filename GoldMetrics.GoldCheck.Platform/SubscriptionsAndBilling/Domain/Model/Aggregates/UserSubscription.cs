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
    
    public void RequestDowngrade(RequestDowngradeCommand command)
    {
        RequestedDowngradePlan = command.NewPlanType;
        SubscriptionStatus = new SubscriptionStatus("DowngradeRequested");
        Status = "DowngradeRequested";
    }
    
    public void ExecuteDowngrade(ExecuteDowngradeCommand command)
    {
        if (RequestedDowngradePlan is not null)
            PlanType = new PlanType(RequestedDowngradePlan);
        RequestedDowngradePlan = null;
        SubscriptionStatus = new SubscriptionStatus("Active");
        Status = "DowngradeExecuted";
    }
    
    public bool DecideAccess(DecideAccessCommand command)
    {
        AccessGranted = AssignedFeatures.Contains(command.FeatureName);
        Status = AccessGranted ? "AccessGranted" : "AccessDenied";
        return AccessGranted;
    }
    
    public bool CheckFeatureAccess(CheckFeatureAccessCommand command)
    {
        var hasAccess = AssignedFeatures.Contains(command.FeatureName);
        Status = "FeatureAccessChecked";
        return hasAccess;
    }
    
    public void ViewPaymentHistory(ViewPaymentHistoryCommand command)
    {
        Status = "PaymentHistoryLoaded";
    }
    
    public void RequestPaymentHistory(RequestPaymentHistoryCommand command)
    {
        Status = "PaymentHistoryRequested";
    }
    
    public Invoice GenerateInvoice(GenerateInvoiceCommand command)
    {
        var invoice = new Invoice(command.InvoiceId, 0m);
        Invoices.Add(invoice);
        Status = "InvoiceGenerated";
        return invoice;
    }
    
    public void RequestInvoice(RequestInvoiceCommand command)
    {
        Status = "InvoiceRequested";
    }
    
    public Invoice? DownloadInvoice(DownloadInvoiceCommand command)
    {
        var invoice = Invoices.FirstOrDefault(i => i.InvoiceId.Value == command.InvoiceId);
        invoice?.MarkDownloaded();
        Status = "InvoiceDownloaded";
        return invoice;
    }
    
    public void CheckUserPlan(CheckUserPlanCommand command)
    {
        Status = "UserPlanDefined";
    }
    
    public void RequestAccess(RequestAccessCommand command)
    {
        Status = "AccessRequested";
    }
}