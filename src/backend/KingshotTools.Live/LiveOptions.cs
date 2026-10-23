namespace KingshotTools.Live;

/// <summary>Live-update settings, bound from the <c>Live</c> configuration section.</summary>
public sealed class LiveOptions
{
    public const string SectionName = "Live";

    /// <summary>Run the change feed processor in this instance.</summary>
    public bool ChangeFeedEnabled { get; set; } = true;

    public string ProcessorName { get; set; } = "worldState-to-signalr";

    /// <summary>Lease owner name; defaults to the machine name.</summary>
    public string? InstanceName { get; set; }
}
