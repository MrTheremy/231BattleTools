namespace KingshotTools.Data.Documents;

/// <summary>Lifecycle values for <see cref="GameDocument.Status"/>.</summary>
public static class DocumentStatus
{
    public const string Active = "active";

    /// <summary>
    /// Upserted before the TTL removes a document, because the change feed does not report deletes
    /// (docs/adr/0005).
    /// </summary>
    public const string Ended = "ended";
}
