using GoldMetrics.GoldCheck.Platform.Shared.Domain.Model.Events;

namespace GoldMetrics.GoldCheck.Platform.IncidentManagement.Domain.Model.Events;

public record AccidentCommittedEvent(string IncidentId, string OperatorId, string Description) : IEvent;