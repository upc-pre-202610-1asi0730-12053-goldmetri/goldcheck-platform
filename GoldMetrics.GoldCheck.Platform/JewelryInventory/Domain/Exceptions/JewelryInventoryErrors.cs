namespace GoldMetrics.GoldCheck.Platform.JewelryInventory.Domain.Exceptions;

public static class JewelryInventoryErrors
{
    public static JewelryInventoryError None => JewelryInventoryError.None;
    public static JewelryInventoryError MaterialNotFound => JewelryInventoryError.MaterialNotFound;
    public static JewelryInventoryError MaterialAlreadyCertified => JewelryInventoryError.MaterialAlreadyCertified;
    public static JewelryInventoryError InvalidQRCode => JewelryInventoryError.InvalidQRCode;
    public static JewelryInventoryError InvalidStatus => JewelryInventoryError.InvalidStatus;
    public static JewelryInventoryError OperationCancelled => JewelryInventoryError.OperationCancelled;
    public static JewelryInventoryError DatabaseError => JewelryInventoryError.DatabaseError;
    public static JewelryInventoryError InternalServerError => JewelryInventoryError.InternalServerError;
}
