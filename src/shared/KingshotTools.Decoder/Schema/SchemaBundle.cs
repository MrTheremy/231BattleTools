namespace KingshotTools.Decoder.Schema;

/// <summary>A versioned set of sproto message definitions extracted from a game build.</summary>
/// <param name="Version">Game/schema version the bundle was extracted from.</param>
/// <param name="MessageNames">Known message ids and their names.</param>
public sealed record SchemaBundle(string Version, IReadOnlyDictionary<int, string> MessageNames)
{
    public static SchemaBundle Empty { get; } = new("none", new Dictionary<int, string>());
}
