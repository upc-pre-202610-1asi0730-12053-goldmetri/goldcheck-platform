using GoldMetrics.GoldCheck.Platform.Shared.Domain.Model.Events;

namespace GoldMetrics.GoldCheck.Platform.JewelryInventory.Domain.Model.Events;

public record CertificateSavedEvent(string CertificateId, string JewelerId) : IEvent;
