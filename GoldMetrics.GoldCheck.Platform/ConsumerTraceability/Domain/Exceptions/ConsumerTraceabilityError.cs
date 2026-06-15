namespace GoldMetrics.GoldCheck.Platform.ConsumerTraceability.Domain.Exceptions;

public enum ConsumerTraceabilityError
{
    None,
    ProductNotFound,
    InvalidQRCode,
    OperationCancelled,
    DatabaseError,
    InternalServerError,
    LanguageNotSupported
}