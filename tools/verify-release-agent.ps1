#!/usr/bin/env pwsh
<#
.SYNOPSIS
  Proves that a Release build of the agent does not contain KingshotTools.Decoder (docs/adr/0003).

.DESCRIPTION
  1. Builds KingshotTools.Agent in Release. The project's AssertReleaseExcludesDecoder target already
     fails the build if the decoder is resolved by any route.
  2. Inspects the build output: no KingshotTools.Decoder.dll, and no mention in the .deps.json.
  Runs on Windows and (thanks to EnableWindowsTargeting) on Linux/macOS.
#>
$ErrorActionPreference = 'Stop'
Set-StrictMode -Version Latest

$root = Split-Path -Parent $PSScriptRoot
$project = Join-Path $root 'src/agent/KingshotTools.Agent/KingshotTools.Agent.csproj'
$output = Join-Path $root 'artifacts/verify-release-agent'

if (Test-Path $output) { Remove-Item $output -Recurse -Force }

dotnet build $project -c Release -o $output --nologo
if ($LASTEXITCODE -ne 0) { throw "Release build of the agent failed." }

$problems = @()

$dlls = Get-ChildItem $output -Recurse -Filter 'KingshotTools.Decoder*.dll'
if ($dlls) { $problems += "Decoder assembly found in output: $($dlls.FullName -join ', ')" }

$deps = Join-Path $output 'KingshotTools.Agent.deps.json'
if (-not (Test-Path $deps)) { throw "Expected $deps to exist." }
if (Select-String -Path $deps -Pattern 'KingshotTools.Decoder' -SimpleMatch -Quiet) {
    $problems += "KingshotTools.Agent.deps.json references KingshotTools.Decoder."
}

if ($problems) {
    $problems | ForEach-Object { Write-Error $_ -ErrorAction Continue }
    exit 1
}

Write-Host "OK: Release agent build contains no reference to KingshotTools.Decoder."
