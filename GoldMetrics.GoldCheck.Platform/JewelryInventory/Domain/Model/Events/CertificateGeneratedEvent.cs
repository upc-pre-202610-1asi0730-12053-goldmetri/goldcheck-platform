using GoldMetrics.GoldCheck.Platform.Shared.Domain.Model.Events;

namespace GoldMetrics.GoldCheck.Platform.JewelryInventory.Domain.Model.Events;

public record CertificateGeneratedEvent(string MaterialId, string CertificateId) : IEvent;
