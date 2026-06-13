namespace GoldMetrics.GoldCheck.Platform.ReportingNotifications.Domain.Model.Aggregates;

public class Report
{
    public void LoadAccidentData(LoadAccidentDataCommand command)
    {
        ReportStatus = new ReportStatus("DataLoaded");
        Status = "AccidentDataLoaded";
    }
}