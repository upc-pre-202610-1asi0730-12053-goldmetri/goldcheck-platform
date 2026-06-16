namespace GoldMetrics.GoldCheck.Platform.JewelryInventory.Domain.Exceptions;

public enum JewelryInventoryError
{
    None,
    MaterialNotFound,
    MaterialAlreadyCertified,
    InvalidQRCode,
    InvalidStatus,
    OperationCancelled,
    DatabaseError,
    InternalServerError
}
