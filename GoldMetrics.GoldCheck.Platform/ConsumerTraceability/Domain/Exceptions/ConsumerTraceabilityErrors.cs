namespace GoldMetrics.GoldCheck.Platform.ConsumerTraceability.Domain.Exceptions;

public static class ConsumerTraceabilityErrors
{
    public static ConsumerTraceabilityError None => ConsumerTraceabilityError.None;
    public static ConsumerTraceabilityError ProductNotFound => ConsumerTraceabilityError.ProductNotFound;
    public static ConsumerTraceabilityError InvalidQRCode => ConsumerTraceabilityError.InvalidQRCode;
    public static ConsumerTraceabilityError OperationCancelled => ConsumerTraceabilityError.OperationCancelled;
    public static ConsumerTraceabilityError DatabaseError => ConsumerTraceabilityError.DatabaseError;
    public static ConsumerTraceabilityError InternalServerError => ConsumerTraceabilityError.InternalServerError;
    public static ConsumerTraceabilityError LanguageNotSupported => ConsumerTraceabilityError.LanguageNotSupported;
}