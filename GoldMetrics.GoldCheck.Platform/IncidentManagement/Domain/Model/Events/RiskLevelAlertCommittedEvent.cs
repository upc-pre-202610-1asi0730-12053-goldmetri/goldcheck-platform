using GoldMetrics.GoldCheck.Platform.Shared.Domain.Model.Events;

namespace GoldMetrics.GoldCheck.Platform.IncidentManagement.Domain.Model.Events;

public record RiskLevelAlertCommittedEvent(string IncidentId) : IEvent;