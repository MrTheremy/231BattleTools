namespace KingshotTools.Agent.Startup;

/// <summary>Parsed agent command line.</summary>
/// <param name="DevMode">True when <c>--dev</c> was passed: the dev console window is available.</param>
public sealed record AgentCommandLine(bool DevMode)
{
    public const string DevFlag = "--dev";

    public static AgentCommandLine Parse(IReadOnlyList<string> args)
    {
        ArgumentNullException.ThrowIfNull(args);

        var devMode = args.Any(a => string.Equals(a, DevFlag, StringComparison.OrdinalIgnoreCase));
        return new AgentCommandLine(devMode);
    }
}
