namespace KingshotTools.Agent.Capture.Devices;

/// <summary>A network interface that yields TCP segments for the configured ports.</summary>
public interface ICaptureDevice : IAsyncDisposable
{
    string Name { get; }

    string Description { get; }

    /// <summary>Opens the device and streams segments until <paramref name="cancellationToken"/> fires.</summary>
    IAsyncEnumerable<RawSegment> CaptureAsync(CaptureOptions options, CancellationToken cancellationToken);
}
