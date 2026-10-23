namespace KingshotTools.Decoder.Schema;

/// <summary>Supplies the active schema bundle to the decoders.</summary>
public interface ISchemaBundleProvider
{
    SchemaBundle Current { get; }
}
