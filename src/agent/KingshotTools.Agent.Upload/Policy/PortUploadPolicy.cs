using KingshotTools.Contracts.Frames;
using KingshotTools.Contracts.Protocol;

namespace KingshotTools.Agent.Upload.Policy;

/// <summary>
/// Default policy: game data (sproto) is uploaded, chat (FPNN) never has its body uploaded,
/// everything else is dropped.
/// </summary>
public sealed class PortUploadPolicy : IUploadPolicy
{
    public UploadDecision Decide(CapturedFrame frame)
    {
        ArgumentNullException.ThrowIfNull(frame);

        return ProtocolPorts.Classify(frame.Port) switch
        {
            ProtocolKind.Sproto => UploadDecision.Upload,
            ProtocolKind.Fpnn => UploadDecision.HeaderOnly,
            _ => UploadDecision.Drop,
        };
    }
}
