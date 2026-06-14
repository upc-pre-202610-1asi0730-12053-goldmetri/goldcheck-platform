using GoldMetrics.GoldCheck.Platform.Shared.Domain.Model.Events;

namespace GoldMetrics.GoldCheck.Platform.ReportingNotifications.Domain.Model.Events;

public record ReportExportationRequestedEvent(string ReportId, string Format) : IEvent;