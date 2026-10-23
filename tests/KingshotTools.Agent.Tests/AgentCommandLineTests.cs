using KingshotTools.Agent.Startup;

namespace KingshotTools.Agent.Tests;

public class AgentCommandLineTests
{
    [Fact]
    public void Defaults_to_tray_only() => Assert.False(AgentCommandLine.Parse([]).DevMode);

    [Theory]
    [InlineData("--dev")]
    [InlineData("--DEV")]
    public void Dev_flag_enables_dev_console(string flag) => Assert.True(AgentCommandLine.Parse([flag]).DevMode);

    [Fact]
    public void Unknown_arguments_are_ignored() => Assert.False(AgentCommandLine.Parse(["--verbose"]).DevMode);
}
