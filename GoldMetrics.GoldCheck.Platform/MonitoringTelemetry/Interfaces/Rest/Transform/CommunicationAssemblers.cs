using GoldMetrics.GoldCheck.Platform.MonitoringTelemetry.Domain.Model.Aggregates;
using GoldMetrics.GoldCheck.Platform.MonitoringTelemetry.Domain.Model.Commands;
using GoldMetrics.GoldCheck.Platform.MonitoringTelemetry.Interfaces.Rest.Resources;

namespace GoldMetrics.GoldCheck.Platform.MonitoringTelemetry.Interfaces.Rest.Transform;

public static class MonitorCommunicationCommandFromResourceAssembler
{
    public static MonitorCommunicationChannelCommand ToCommandFromResource(MonitorCommunicationChannelResource resource)
        => new(resource.AssetId);
}

public static class CommunicationChannelResourceFromEntityAssembler
{
    public static CommunicationChannelResource ToResourceFromEntity(CommunicationChannel entity)
        => new(entity.Id, entity.AssetId.Value, entity.Status,
            entity.LastAnalysedProtocol, entity.AnomalyProtocol, entity.AnomalyDescription,
            entity.CreatedAt, entity.UpdatedAt);
    
    public static class AnalyseCommunicationCommandFromResourceAssembler
    {
        public static AnalyseCommunicationCommand ToCommandFromResource(string assetId, AnalyseCommunicationResource resource)
            => new(assetId, resource.Protocol);
    }
    
    public static class DetectCommunicationAnomalyCommandFromResourceAssembler
    {
        public static DetectCommunicationAnomalyCommand ToCommandFromResource(string assetId, DetectCommunicationAnomalyResource resource)
            => new(assetId, resource.Protocol, resource.AnomalyDescription);
    }
}