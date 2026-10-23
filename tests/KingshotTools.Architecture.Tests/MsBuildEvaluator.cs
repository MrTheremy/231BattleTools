using System.Diagnostics;
using System.Text.Json;

namespace KingshotTools.Architecture.Tests;

/// <summary>Evaluates a project with <c>dotnet msbuild -getItem</c>, exactly as a build in that configuration would see it.</summary>
internal static class MsBuildEvaluator
{
    public static string RepoRoot { get; } = FindRepoRoot();

    public static IReadOnlyList<string> GetProjectReferences(string relativeProjectPath, string configuration)
    {
        var project = Path.Combine(RepoRoot, relativeProjectPath);
        var startInfo = new ProcessStartInfo(DotnetExecutable())
        {
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false,
            WorkingDirectory = RepoRoot,
        };
        foreach (var arg in new[] { "msbuild", project, "-getItem:ProjectReference", $"-property:Configuration={configuration}", "-nologo" })
        {
            startInfo.ArgumentList.Add(arg);
        }

        using var process = Process.Start(startInfo)!;
        var stdout = process.StandardOutput.ReadToEnd();
        var stderr = process.StandardError.ReadToEnd();
        process.WaitForExit();

        if (process.ExitCode != 0)
        {
            throw new InvalidOperationException($"dotnet msbuild failed ({process.ExitCode}):\n{stdout}\n{stderr}");
        }

        using var json = JsonDocument.Parse(stdout);
        if (!json.RootElement.GetProperty("Items").TryGetProperty("ProjectReference", out var items))
        {
            return [];
        }

        return [.. items.EnumerateArray().Select(i => i.GetProperty("Identity").GetString()!)];
    }

    private static string DotnetExecutable() =>
        Environment.GetEnvironmentVariable("DOTNET_HOST_PATH") is { Length: > 0 } hostPath && File.Exists(hostPath)
            ? hostPath
            : "dotnet";

    private static string FindRepoRoot()
    {
        for (var dir = new DirectoryInfo(AppContext.BaseDirectory); dir is not null; dir = dir.Parent)
        {
            if (File.Exists(Path.Combine(dir.FullName, "KingshotTools.sln")))
            {
                return dir.FullName;
            }
        }

        throw new InvalidOperationException("Could not find KingshotTools.sln above the test output directory.");
    }
}
