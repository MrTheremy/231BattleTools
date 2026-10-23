namespace KingshotTools.Agent.Upload;

/// <summary>Settings for spooling and uploading frames.</summary>
public sealed class UploadOptions
{
    public UploadTarget Target { get; set; } = UploadTarget.Off;

    /// <summary>Ingest endpoint of a backend running on this machine.</summary>
    public Uri LocalEndpoint { get; set; } = new("ws://localhost:5080/ingest/ws");

    /// <summary>Production ingest endpoint (TLS).</summary>
    public Uri? ProdEndpoint { get; set; }

    public string SpoolDirectory { get; set; } =
        Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "KingshotTools", "spool");

    public int MaxFramesPerBatch { get; set; } = 500;

    public TimeSpan MaxBatchDelay { get; set; } = TimeSpan.FromSeconds(1);
}
