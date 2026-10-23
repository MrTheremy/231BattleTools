using KingshotTools.Contracts.Frames;

namespace KingshotTools.Agent.Upload.Policy;

/// <summary>Decides what part of a captured frame may leave the PC.</summary>
public interface IUploadPolicy
{
    UploadDecision Decide(CapturedFrame frame);
}
