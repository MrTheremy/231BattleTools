namespace KingshotTools.Architecture.Tests;

/// <summary>docs/adr/0003: Release builds of the agent must not include the decoder.</summary>
public class DecoderGatingTests
{
    private const string Decoder = "KingshotTools.Decoder";

    public static TheoryData<string> AgentProjects { get; } = new()
    {
        Path.Combine("src", "agent", "KingshotTools.Agent", "KingshotTools.Agent.csproj"),
        Path.Combine("src", "agent", "KingshotTools.Agent.Capture", "KingshotTools.Agent.Capture.csproj"),
        Path.Combine("src", "agent", "KingshotTools.Agent.Upload", "KingshotTools.Agent.Upload.csproj"),
    };

    [Theory]
    [MemberData(nameof(AgentProjects))]
    public void Release_agent_projects_do_not_reference_the_decoder(string project)
    {
        var references = MsBuildEvaluator.GetProjectReferences(project, "Release");

        Assert.DoesNotContain(references, r => r.Contains(Decoder, StringComparison.OrdinalIgnoreCase));
    }

    [Fact]
    public void Debug_agent_references_the_decoder_for_the_dev_console()
    {
        // Proves the Release test above is not vacuous: the gate really is configuration-dependent.
        var references = MsBuildEvaluator.GetProjectReferences(AgentProjects.First().Data, "Debug");

        Assert.Contains(references, r => r.Contains(Decoder, StringComparison.OrdinalIgnoreCase));
    }

    [Theory]
    [InlineData("Debug")]
    [InlineData("Release")]
    public void Contracts_has_no_project_references(string configuration) =>
        Assert.Empty(MsBuildEvaluator.GetProjectReferences(
            Path.Combine("src", "shared", "KingshotTools.Contracts", "KingshotTools.Contracts.csproj"),
            configuration));
}
