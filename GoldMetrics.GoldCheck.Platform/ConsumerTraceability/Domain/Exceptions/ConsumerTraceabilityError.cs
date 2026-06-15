namespace GoldMetrics.GoldCheck.Platform.ConsumerTraceability.Domain.Exceptions;

public enum ConsumerTraceabilityError
{
    None,
    ProductNotFound,
    InvalidQrCode,
    OperationCancelled,
    DatabaseError,
    InternalServerError,
    LanguageNotSupported,
    CertificateNotFound,
    AccessDenied
}