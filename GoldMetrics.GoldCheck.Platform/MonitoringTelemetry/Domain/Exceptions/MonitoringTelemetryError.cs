namespace GoldMetrics.GoldCheck.Platform.MonitoringTelemetry.Domain.Exceptions;

public enum MonitoringTelemetryError
{
    None,
    AssetNotFound,
    TelemetryDataNotFound,
    TemperatureReadingNotFound,
    CommunicationChannelNotFound,
    GNSSStatusNotFound,
    SpeedReadingNotFound,
    PressureReadingNotFound,
    InvalidTemperature,
    InvalidSpeed,
    InvalidPressure,
    InvalidProtocol,
    InvalidAnomalyType,
    InvalidPressureType,
    OperationCancelled,
    DatabaseError,
    InternalServerError
}