namespace GoldMetrics.GoldCheck.Platform.SubscriptionsAndBilling.Interfaces.Rest.Resources;
public record InvoiceResponse(int Id, string InvoiceId, decimal Amount, string Status);