namespace KingshotTools.Contracts.Frames;

/// <summary>A batch of frames uploaded by one agent over the ingest WebSocket.</summary>
/// <param name="AgentId">Stable id of the installed agent.</param>
/// <param name="SessionId">Id of the capture session (one per agent start / capture start).</param>
/// <param name="SequenceNumber">Monotonic per session; lets the backend detect gaps and duplicates.</param>
/// <param name="Frames">Frames in capture order.</param>
public sealed record FrameBatch(
    string AgentId,
    string SessionId,
    long SequenceNumber,
    IReadOnlyList<CapturedFrame> Frames);
