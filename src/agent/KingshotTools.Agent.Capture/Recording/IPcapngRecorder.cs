using KingshotTools.Agent.Capture.Devices;

namespace KingshotTools.Agent.Capture.Recording;

/// <summary>
/// Writes and replays local .pcapng captures for the dev console.
/// Files stay on this PC; they are never uploaded (docs/adr/0004).
/// </summary>
public interface IPcapngRecorder
{
    Task StartRecordingAsync(string path, CancellationToken cancellationToken);

    Task StopRecordingAsync();

    IAsyncEnumerable<RawSegment> ReplayAsync(string path, CancellationToken cancellationToken);
}
