namespace GoldMetrics.GoldCheck.Platform.Analytics.Domain.Model.Errors;

public enum AnalyticsError
{
    None,
    OperationCancelled,
    DatabaseError,
    InternalServerError,
    MaterialNotFound,
    RouteNotFound
}