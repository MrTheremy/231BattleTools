namespace KingshotTools.Decoder.Schema;

/// <summary>Loads a schema bundle from a file on disk.</summary>
public sealed class FileSchemaBundleProvider : ISchemaBundleProvider
{
    public FileSchemaBundleProvider(string? bundlePath)
    {
        BundlePath = bundlePath;
        // TODO: decide the bundle format (sproto text vs. pre-compiled JSON) and load it here.
        Current = SchemaBundle.Empty;
    }

    public string? BundlePath { get; }

    public SchemaBundle Current { get; }
}
