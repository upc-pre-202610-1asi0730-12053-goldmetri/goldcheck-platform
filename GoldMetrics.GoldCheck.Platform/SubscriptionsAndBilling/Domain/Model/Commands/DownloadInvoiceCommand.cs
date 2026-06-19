namespace GoldMetrics.GoldCheck.Platform.SubscriptionsAndBilling.Domain.Model.Commands;
public record DownloadInvoiceCommand(string UserId, string InvoiceId);