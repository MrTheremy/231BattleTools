namespace KingshotTools.Agent.Upload;

/// <summary>Where captured frames are sent.</summary>
public enum UploadTarget
{
    /// <summary>Nothing is uploaded; frames are dropped after local use.</summary>
    Off = 0,

    /// <summary>Backend on this machine (development).</summary>
    Local = 1,

    /// <summary>Production backend on Azure.</summary>
    Prod = 2,
}
