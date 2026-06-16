namespace GoldMetrics.GoldCheck.Platform.JewelryInventory.Domain.Exceptions;

public enum JewelryInventoryError
{
    None,
    MaterialNotFound,
    MaterialAlreadyCertified,
    OperationCancelled,
    DatabaseError,
    InternalServerError
}
