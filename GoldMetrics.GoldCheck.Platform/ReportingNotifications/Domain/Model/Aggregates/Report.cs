using GoldMetrics.GoldCheck.Platform.ReportingNotifications.Domain.Model.Commands;
using GoldMetrics.GoldCheck.Platform.ReportingNotifications.Domain.Model.ValueObjects;

namespace GoldMetrics.GoldCheck.Platform.ReportingNotifications.Domain.Model.Aggregates;

public partial class Report
{
    public Report()
    {
        SupervisorId = new SupervisorId();
        ReportStatus = new ReportStatus();
        IncidentId = string.Empty;
        Status = string.Empty;
    }

    public Report(RequestAccidentDataCommand command)
    {
        SupervisorId = new SupervisorId(command.SupervisorId);
        IncidentId = command.IncidentId;
        ReportStatus = new ReportStatus("Requested");
        Status = "AccidentDataRequested";
    }

    public int Id { get; }
    public SupervisorId SupervisorId { get; private set; }
    public string IncidentId { get; private set; }
    public ReportStatus ReportStatus { get; private set; }
    public string? DownloadedByUserId { get; private set; }
    public string Status { get; private set; }

    public void LoadAccidentData(LoadAccidentDataCommand command)
    {
        ReportStatus = new ReportStatus("DataLoaded");
        Status = "AccidentDataLoaded";
    }
    public void GenerateReport(GenerateReportCommand command)
    {
        ReportStatus = new ReportStatus("Generated");
        Status = "ReportGenerated";
    }

}
