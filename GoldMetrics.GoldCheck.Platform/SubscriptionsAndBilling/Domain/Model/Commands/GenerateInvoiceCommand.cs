namespace GoldMetrics.GoldCheck.Platform.SubscriptionsAndBilling.Domain.Model.Commands;
public record GenerateInvoiceCommand(string UserId, string InvoiceId);