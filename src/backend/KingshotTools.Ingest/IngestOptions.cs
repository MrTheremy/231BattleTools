namespace KingshotTools.Ingest;

/// <summary>Ingest settings, bound from the <c>Ingest</c> configuration section.</summary>
public sealed class IngestOptions
{
    public const string SectionName = "Ingest";

    /// <summary>Batches buffered between the WebSocket endpoint and the decoder worker before back-pressure applies.</summary>
    public int QueueCapacity { get; set; } = 1_000;

    /// <summary>Largest accepted WebSocket message (one batch), in bytes.</summary>
    public int MaxMessageBytes { get; set; } = 4 * 1024 * 1024;

    /// <summary>Write raw batches to Blob Storage before decoding.</summary>
    public bool ArchiveRawFrames { get; set; } = true;

    /// <summary>Path to the sproto schema bundle used by the decoder.</summary>
    public string? SchemaBundlePath { get; set; }
}
