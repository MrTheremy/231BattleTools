using KingshotTools.Agent.DevConsole;
using KingshotTools.Agent.DevConsole.DecodePreview;
using KingshotTools.Agent.Upload;

namespace KingshotTools.Agent.Startup;

/// <summary>Hand-written composition root. Every external boundary is created here behind an interface.</summary>
public sealed class AgentComposition
{
    public UploadOptions UploadOptions { get; } = new();

    public TimeProvider Clock { get; } = TimeProvider.System;

    public IDecodePreview CreateDecodePreview()
    {
#if DEV_DECODER
        return new DecoderDecodePreview();
#else
        return new UnavailableDecodePreview();
#endif
    }

    public DevConsoleViewModel CreateDevConsoleViewModel() => new(CreateDecodePreview(), UploadOptions);
}
