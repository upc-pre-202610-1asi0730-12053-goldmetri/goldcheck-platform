using GoldMetrics.GoldCheck.Platform.Shared.Domain.Model.Events;

namespace GoldMetrics.GoldCheck.Platform.ReportingNotifications.Domain.Model.Events;

public record AccidentDataRequestedEvent(string ReportId, string IncidentId, string SupervisorId) : IEvent;