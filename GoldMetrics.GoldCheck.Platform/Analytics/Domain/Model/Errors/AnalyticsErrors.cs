namespace GoldMetrics.GoldCheck.Platform.Analytics.Domain.Model.Errors;

public static class AnalyticsErrors
{
    public static AnalyticsError None => AnalyticsError.None;
    public static AnalyticsError OperationCancelled => AnalyticsError.OperationCancelled;
    public static AnalyticsError DatabaseError => AnalyticsError.DatabaseError;
    public static AnalyticsError InternalServerError => AnalyticsError.InternalServerError;
    public static AnalyticsError MaterialNotFound => AnalyticsError.MaterialNotFound;
    public static AnalyticsError RouteNotFound => AnalyticsError.RouteNotFound;
    public static AnalyticsError InvalidProductionPeriod => AnalyticsError.InvalidProductionPeriod;
    public static AnalyticsError InsufficientData => AnalyticsError.InsufficientData;
    public static AnalyticsError ProductionDataValidationFailed => AnalyticsError.ProductionDataValidationFailed;
}