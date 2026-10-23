namespace KingshotTools.Data.Options;

/// <summary>Cosmos DB settings, bound from the <c>Cosmos</c> configuration section.</summary>
public sealed class CosmosOptions
{
    public const string SectionName = "Cosmos";

    /// <summary>Account connection string. Locally this is the emulator; in Azure use user-secrets / Key Vault (TODO: managed identity).</summary>
    public string ConnectionString { get; set; } = string.Empty;

    public string DatabaseName { get; set; } = "kingshot";

    /// <summary>Create the database and containers on startup. Only honoured in the Development environment.</summary>
    public bool EnsureCreatedOnStartup { get; set; }

    /// <summary>Gateway mode with a single endpoint; required by the local emulator.</summary>
    public bool UseEmulatorConnectionMode { get; set; }
}
