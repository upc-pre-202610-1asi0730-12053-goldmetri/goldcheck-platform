using GoldMetrics.GoldCheck.Platform.MaterialOperations.Domain.Model.Commands;
using GoldMetrics.GoldCheck.Platform.MaterialOperations.Domain.Model.ValueObjects;

namespace GoldMetrics.GoldCheck.Platform.MaterialOperations.Domain.Model.Aggregates;

public partial class Material
{
    public Material()
    {
        BatchId = new MaterialBatchId();
        MineralType = new MineralType();
        Payload = new Payload();
        Status = string.Empty;
    }

    public Material(IdentifyMineralTypeCommand command)
    {
        BatchId = new MaterialBatchId(command.BatchId);
        MineralType = new MineralType(command.MineralType);
        Payload = new Payload(command.PayloadTons);
        Status = "Identified";
    }

    public int Id { get; }
    public MaterialBatchId BatchId { get; private set; }
    public MineralType MineralType { get; private set; }
    public Payload Payload { get; private set; }
    public string Status { get; private set; }
}
