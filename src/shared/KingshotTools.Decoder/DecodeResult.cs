using System.Text.Json.Nodes;
using KingshotTools.Contracts.Protocol;

namespace KingshotTools.Decoder;

/// <summary>Outcome of decoding a single frame.</summary>
/// <param name="Protocol">Protocol the frame was routed to.</param>
/// <param name="Status">Whether decoding succeeded.</param>
/// <param name="MessageId">Protocol message id, when the header could be read.</param>
/// <param name="MessageName">Schema name for <paramref name="MessageId"/>, when known.</param>
/// <param name="Body">Decoded message as JSON, when decoding succeeded.</param>
/// <param name="Error">Human-readable reason when <paramref name="Status"/> is not <see cref="DecodeStatus.Decoded"/>.</param>
public sealed record DecodeResult(
    ProtocolKind Protocol,
    DecodeStatus Status,
    int? MessageId = null,
    string? MessageName = null,
    JsonObject? Body = null,
    string? Error = null)
{
    public static DecodeResult NotImplemented(ProtocolKind protocol) =>
        new(protocol, DecodeStatus.NotImplemented, Error: "Decoding is not implemented yet.");
}

public enum DecodeStatus
{
    Decoded = 0,

    /// <summary>Header read, but the message id is not in the schema bundle (feeds the Protocol Lab).</summary>
    UnknownMessage = 1,

    Malformed = 2,
    Ignored = 3,
    NotImplemented = 4,
}
