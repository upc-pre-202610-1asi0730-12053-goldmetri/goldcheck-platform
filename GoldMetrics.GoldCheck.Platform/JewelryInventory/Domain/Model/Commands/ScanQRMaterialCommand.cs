namespace GoldMetrics.GoldCheck.Platform.JewelryInventory.Domain.Model.Commands;

public record ScanQRMaterialCommand(string MaterialId, string QRCode);
