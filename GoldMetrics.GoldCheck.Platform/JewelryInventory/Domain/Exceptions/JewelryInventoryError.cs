namespace GoldMetrics.GoldCheck.Platform.JewelryInventory.Domain.Exceptions;

public enum JewelryInventoryError
{
    None,
    MaterialNotFound,
    CertificateNotFound,
    MaterialAlreadyCertified,
    InvalidQRCode,
    InvalidStatus,
    OperationCancelled,
    DatabaseError,
    InternalServerError
}
