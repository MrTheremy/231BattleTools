namespace KingshotTools.Data.Options;

/// <summary>Blob Storage settings, bound from the <c>Blob</c> configuration section.</summary>
public sealed class BlobOptions
{
    public const string SectionName = "Blob";

    /// <summary>Storage connection string. Locally this is Azurite.</summary>
    public string ConnectionString { get; set; } = string.Empty;

    /// <summary>Container that holds archived raw frame batches.</summary>
    public string RawFramesContainer { get; set; } = "raw-frames";

    /// <summary>Create the container on startup. Only honoured in the Development environment.</summary>
    public bool EnsureCreatedOnStartup { get; set; }
}
