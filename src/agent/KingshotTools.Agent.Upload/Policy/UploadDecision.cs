namespace KingshotTools.Agent.Upload.Policy;

public enum UploadDecision
{
    /// <summary>Never leaves the PC.</summary>
    Drop = 0,

    /// <summary>Upload the frame as captured.</summary>
    Upload = 1,

    /// <summary>Upload only the protocol header; the body is stripped first (FPNN chat).</summary>
    HeaderOnly = 2,
}
