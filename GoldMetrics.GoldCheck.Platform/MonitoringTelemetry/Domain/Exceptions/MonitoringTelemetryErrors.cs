namespace GoldMetrics.GoldCheck.Platform.MonitoringTelemetry.Domain.Exceptions;

public static class MonitoringTelemetryErrors
{
    public static MonitoringTelemetryError AssetNotFound => MonitoringTelemetryError.AssetNotFound;
    public static MonitoringTelemetryError TelemetryDataNotFound => MonitoringTelemetryError.TelemetryDataNotFound;
    public static MonitoringTelemetryError TemperatureReadingNotFound => MonitoringTelemetryError.TemperatureReadingNotFound;
    public static MonitoringTelemetryError CommunicationChannelNotFound => MonitoringTelemetryError.CommunicationChannelNotFound;
    public static MonitoringTelemetryError GNSSStatusNotFound => MonitoringTelemetryError.GNSSStatusNotFound;
    public static MonitoringTelemetryError SpeedReadingNotFound => MonitoringTelemetryError.SpeedReadingNotFound;
    public static MonitoringTelemetryError PressureReadingNotFound => MonitoringTelemetryError.PressureReadingNotFound;
    public static MonitoringTelemetryError InvalidTemperature => MonitoringTelemetryError.InvalidTemperature;
    public static MonitoringTelemetryError InvalidSpeed => MonitoringTelemetryError.InvalidSpeed;
    public static MonitoringTelemetryError InvalidPressure => MonitoringTelemetryError.InvalidPressure;
    public static MonitoringTelemetryError InvalidProtocol => MonitoringTelemetryError.InvalidProtocol;
    public static MonitoringTelemetryError InvalidAnomalyType => MonitoringTelemetryError.InvalidAnomalyType;
    public static MonitoringTelemetryError InvalidPressureType => MonitoringTelemetryError.InvalidPressureType;
    public static MonitoringTelemetryError OperationCancelled => MonitoringTelemetryError.OperationCancelled;
    public static MonitoringTelemetryError DatabaseError => MonitoringTelemetryError.DatabaseError;
    public static MonitoringTelemetryError InternalServerError => MonitoringTelemetryError.InternalServerError;
}