namespace GoldMetrics.GoldCheck.Platform.MonitoringTelemetry.Domain.Model.ValueObjects;

public record CommunicationProtocol
{
    private static readonly HashSet<string> AllowedProtocols =
        new(StringComparer.OrdinalIgnoreCase)
            { "RS232", "CDL", "Modbus", "Cellular", "OPC", "CANOpen", "EthernetIP" };

    public CommunicationProtocol() => Value = string.Empty;

    public CommunicationProtocol(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("CommunicationProtocol cannot be empty.", nameof(value));
        if (!AllowedProtocols.Contains(value))
            throw new ArgumentException(
                $"Invalid protocol '{value}'. Allowed: RS232, CDL, Modbus, Cellular, OPC, CANOpen, EthernetIP.",
                nameof(value));
        Value = value;
    }

    public string Value { get; init; }
}