# 0003. Decoder excluded from the Release agent

- Status: accepted
- Date: 2026-10-23

## Context

`KingshotTools.Decoder` holds our protocol knowledge: sproto schemas, message ids and mappings. The backend needs
it to decode uploads. The agent's dev console uses it for a local decode preview. Shipping it inside the
distributed agent would hand that knowledge to anyone who installs the agent, and it would tie agent releases to
schema changes.

## Decision

Release builds of the agent only capture and upload frames. Decoding happens on the backend.

- `KingshotTools.Agent.csproj` adds the `ProjectReference` to the decoder only when `Configuration != Release`,
  and defines `DEV_DECODER` for those builds. Code that uses the decoder sits behind `#if DEV_DECODER`.
- The `AssertReleaseExcludesDecoder` MSBuild target fails a Release build if the decoder assembly is resolved
  by any route, direct or transitive.
- `tests/KingshotTools.Architecture.Tests` evaluates the agent projects with MSBuild and asserts the
  configuration-dependent references.
- CI runs `tools/verify-release-agent.ps1`, which inspects the Release output and `.deps.json`.

## Consequences

- In Release builds the dev console shows "Decoder not included in this build" in the decode preview.
- Contracts must never depend on the decoder, because the agent needs Contracts in every configuration.
