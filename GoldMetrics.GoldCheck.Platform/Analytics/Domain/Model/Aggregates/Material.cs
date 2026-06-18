using GoldMetrics.GoldCheck.Platform.Analytics.Domain.Model.Commands;
using GoldMetrics.GoldCheck.Platform.Analytics.Domain.Model.ValueObjects;

namespace GoldMetrics.GoldCheck.Platform.Analytics.Domain.Model.Aggregates;

public partial class Material
{
    public Material()
    {
        MaterialId = new MaterialId();
        RouteId = new RouteId();
        RouteStatus = new RouteStatus();
        SupervisorId = new SupervisorId();
        UserId = new UserId();
        Status = string.Empty;
    }

    public Material(ViewRouteProgressCommand command)
    {
        MaterialId = new MaterialId(Guid.NewGuid().ToString());
        RouteId = new RouteId(command.RouteId);
        UserId = new UserId(command.UserId);
        SupervisorId = new SupervisorId();
        RouteStatus = new RouteStatus("InProgress");
        Status = "RouteDataLoaded";
    }

    public int Id { get; }
    public MaterialId MaterialId { get; private set; }
    public RouteId RouteId { get; private set; }
    public RouteStatus RouteStatus { get; private set; }
    public SupervisorId SupervisorId { get; private set; }
    public UserId UserId { get; private set; }
    public string Status { get; private set; }
    public DateTimeOffset? ProductionStart { get; private set; }
    public DateTimeOffset? ProductionEnd { get; private set; }
    
    public void ViewProductionDashboard(ViewProductionDashboardCommand command)
    {
        SupervisorId = new SupervisorId(command.SupervisorId);
        Status = "DashboardViewed";
    }
    
    public void RequestProductionData(RequestProductionDataCommand command)
    {
        var period = new ProductionPeriod(command.Start, command.End);
        SupervisorId = new SupervisorId(command.SupervisorId);
        ProductionStart = period.Start;
        ProductionEnd = period.End;
        Status = "ProductionDataRequested";
    }
    
    public void LoadProductionData(LoadProductionDataCommand command)
    {
        RouteId = new RouteId(command.RouteId);
        Status = "ProductionDataLoaded";
    }
}